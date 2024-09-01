using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.Entities;
using Domain.Exeptions;
using Domain.IRepositories;
using Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Application.Repositories;

public class UserRepository(AppDbContext context, IConfiguration configuration) : IUserRepository
{
    private readonly AppDbContext context = context;
    private readonly IConfiguration configuration = configuration;

    public User Get(string token)
    {
        if (string.IsNullOrEmpty(token))
        {
            throw new InvalidToken("Provided token is invalid");
        }

        var handler = new JwtSecurityTokenHandler();

        if (!handler.CanReadToken(token))
        {
            throw new InvalidToken("Token cannot be read");
        }

        JwtSecurityToken jwtToken = jwtToken = handler.ReadJwtToken(token);

        var claims = jwtToken.Claims.ToList();
        string userEmail = claims.FirstOrDefault(c => c.Type == "Email")?.Value!;
        string userName = claims.FirstOrDefault(c => c.Type == "UserName")?.Value!;

        if (string.IsNullOrEmpty(userEmail) || string.IsNullOrEmpty(userName))
        {
            throw new InvalidToken("Token does not contain required claims");
        }

        User user = context.Users.FirstOrDefault(u => u.Email == userEmail && u.UserName == userName)! ?? throw new UserNotFound("There is no such user");
        return user;
    }

    public List<User> GetByIds(List<int> AuthorId)
    {
        List<User> users = [.. context.Users.Where(u => AuthorId.Contains(u.Id))];
        if (users == null) return [];
        else return users;
    }
    public string Login(string email, string password)
    {
        User user = context.Users.FirstOrDefault(u => u.Email == email && u.Password == password)!;
        if (user != null)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Jti ,Guid.NewGuid().ToString()),
                new Claim("Email",user.Email),
                new Claim("UserName",user.UserName)
            };
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]!));
            var signIn = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken token = new(
                configuration["JWT:Issuer"],
                configuration["JWT:Audience"],
                claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: signIn
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        else { throw new NotRegesterd($"User wih the email : {email} not registerd"); }
    }
    public void Register(User user)
    {
        context.Users.Add(user);
        context.SaveChanges();
    }
    public void Update(User user, string token)
    {
        if (token != null)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            var claims = jwtToken.Claims;
            string userEmail = claims.FirstOrDefault(c => c.Type == "Email")?.Value!;
            string userName = claims.FirstOrDefault(c => c.Type == "UserName")?.Value!;
            User user1 = context.Users.FirstOrDefault(u => u.Email == userEmail && u.UserName == userName)! ?? throw new UserNotFound("There is no such a user");
            user1.UserName = user.UserName;
            user1.Email = user.Email;
            user1.Password = user.Password;

            context.Users.Update(user1);
            context.SaveChanges();
        }
        else
        {
            throw new InvalidToken("provided token is invalid");
        }
    }

}
