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

/// <summary>
/// Provides data access methods for managing users in the database.
/// </summary>
public class UserRepository(AppDbContext context, IConfiguration configuration) : IUserRepository
{
    private readonly AppDbContext _context = context;
    private readonly IConfiguration _configuration = configuration;

    /// <summary>
    /// Retrieves a user from the database based on the provided JWT token.
    /// </summary>
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

        JwtSecurityToken jwtToken = handler.ReadJwtToken(token);

        var claims = jwtToken.Claims.ToList();
        string userEmail = claims.FirstOrDefault(c => c.Type == "Email")?.Value!;
        string userName = claims.FirstOrDefault(c => c.Type == "UserName")?.Value!;

        if (string.IsNullOrEmpty(userEmail) || string.IsNullOrEmpty(userName))
        {
            throw new InvalidToken("Token does not contain required claims");
        }

        User user = _context.Users.FirstOrDefault(u => u.Email == userEmail && u.UserName == userName)
            ?? throw new UserNotFound("There is no such user");

        return user;
    }

    /// <summary>
    /// Retrieves a list of users by their IDs.
    /// </summary>
    public List<User> GetByIds(List<int> AuthorId)
    {
        List<User> users = _context.Users.Where(u => AuthorId.Contains(u.Id)).ToList();
        return users;
    }

    /// <summary>
    /// Authenticates a user based on email and password, and returns a JWT token if successful.
    /// </summary>
    public string Login(string email, string password)
    {
        User user = _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password)
            ?? throw new NotRegesterd($"User with the email : {email} not registered");

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("Email", user.Email),
            new Claim("UserName", user.UserName)
        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]!));
        var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        JwtSecurityToken token = new(
            _configuration["JWT:Issuer"],
            _configuration["JWT:Audience"],
            claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: signIn
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    /// <summary>
    /// Registers a new user in the database.
    /// </summary>
    public void Register(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
    }

    /// <summary>
    /// Updates the details of an existing user based on the provided JWT token.
    /// </summary>
    public void Update(User user, string token)
    {
        if (string.IsNullOrEmpty(token))
        {
            throw new InvalidToken("Provided token is invalid");
        }

        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);

        var claims = jwtToken.Claims;
        string userEmail = claims.FirstOrDefault(c => c.Type == "Email")?.Value!;
        string userName = claims.FirstOrDefault(c => c.Type == "UserName")?.Value!;
        User existingUser = _context.Users.FirstOrDefault(u => u.Email == userEmail && u.UserName == userName)
            ?? throw new UserNotFound("There is no such user");

        existingUser.UserName = user.UserName;
        existingUser.Email = user.Email;
        existingUser.Password = user.Password;

        _context.Users.Update(existingUser);
        _context.SaveChanges();
    }
}
