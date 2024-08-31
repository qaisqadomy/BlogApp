using Application.Repositories;
using Domain.Entities;
using Domain.Exeptions;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Moq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace RepositoriesTests;

public class UserRepositoryTests
{
    private readonly InMemoryDB _inMemoryDb;
    private readonly UserRepository _userRepository;
    private readonly Mock<IConfiguration> _mockConfig;

    public UserRepositoryTests()
    {
        _inMemoryDb = new InMemoryDB();

        _mockConfig = new Mock<IConfiguration>();
        _mockConfig.SetupGet(x => x["JWT:Key"]).Returns("ThisIsASecretKeyThatIsAtLeast256BitsLong!");
        _mockConfig.SetupGet(x => x["JWT:Issuer"]).Returns("TestIssuer");
        _mockConfig.SetupGet(x => x["JWT:Audience"]).Returns("TestAudience");

        _userRepository = new UserRepository(_inMemoryDb.DbContext, _mockConfig.Object);
    }

    [Fact]
    public void Register_ShouldAddUserToDatabase()
    {

        var user = new User { UserName = "john_doe", Email = "john@example.com", Password = "password123", Bio = "qqq", Image = "dsadsad", Following = false };


        _userRepository.Register(user);
        var result = _inMemoryDb.DbContext.Users.FirstOrDefault(u => u.Email == "john@example.com");


        Assert.NotNull(result);
        Assert.Equal("john_doe", result.UserName);
    }

    [Fact]
    public void Login_ShouldReturnJwtTokenForValidUser()
    {

        var user = new User { UserName = "john_doe", Email = "john@example.com", Password = "password123" };
        _userRepository.Register(user);


        var token = _userRepository.Login("john@example.com", "password123");


        Assert.NotNull(token);
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);
        Assert.Equal("john@example.com", jwtToken.Claims.First(c => c.Type == "Email").Value);
    }

    [Fact]
    public void Login_ShouldThrowExceptionForInvalidUser()
    {

        var exception = Assert.Throws<NotRegesterd>(() => _userRepository.Login("invalid@example.com", "wrongpassword"));
        Assert.Equal("User wih the email : invalid@example.com not registerd", exception.Message);
    }

    [Fact]
    public void Get_ShouldReturnUserForValidToken()
    {

        var user = new User { UserName = "john_doe", Email = "john@example.com", Password = "password123" };
        _userRepository.Register(user);

        var token = _userRepository.Login("john@example.com", "password123");


        var result = _userRepository.Get(token);


        Assert.NotNull(result);
        Assert.Equal("john_doe", result.UserName);
        Assert.Equal("john@example.com", result.Email);
    }

    [Fact]
    public void Get_ShouldThrowExceptionForNonExistentUser()
    {
        var claims = new[]
        {
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim("Email", "non_existent@example.com"),
        new Claim("UserName", "non_existent_user")
    };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ThisIsASecretKeyThatIsAtLeast256BitsLong!"));
        var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: "TestIssuer",
            audience: "TestAudience",
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: signIn
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        var exception = Assert.Throws<UserNotFound>(() => _userRepository.Get(tokenString));
        Assert.Equal("There is no such a user", exception.Message);
    }


    [Fact]
    public void Update_ShouldUpdateUserDetails()
    {

        var user = new User { UserName = "john_doe", Email = "john@example.com", Password = "password123" };
        _userRepository.Register(user);

        var token = _userRepository.Login("john@example.com", "password123");

        var updatedUser = new User { UserName = "john_updated", Email = "john_updated@example.com", Password = "newpassword123" };


        _userRepository.Update(updatedUser, token);


        var newToken = _userRepository.Login("john_updated@example.com", "newpassword123");

        var result = _userRepository.Get(newToken);


        Assert.NotNull(result);
        Assert.Equal("john_updated", result.UserName);
        Assert.Equal("john_updated@example.com", result.Email);
        Assert.Equal("newpassword123", result.Password);
    }


    [Fact]
    public void Update_ShouldThrowExceptionWhenUserNotFound()
    {
        // Arrange: Create a valid JWT with claims that do not correspond to any existing user
        var invalidClaims = new[]
        {
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim("Email", "non_existent@example.com"),
        new Claim("UserName", "non_existent_user")
    };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ThisIsASecretKeyThatIsAtLeast256BitsLong!"));
        var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: "TestIssuer",
            audience: "TestAudience",
            claims: invalidClaims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: signIn
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        var updatedUser = new User { UserName = "john_updated", Email = "john_updated@example.com", Password = "newpassword123" };

        // Act & Assert: Expect UserNotFound exception when updating with a non-existent user
        var exception = Assert.Throws<UserNotFound>(() => _userRepository.Update(updatedUser, tokenString));
        Assert.Equal("There is no such a user", exception.Message);
    }



    public void Dispose()
    {
        _inMemoryDb.Dispose();
    }

}
