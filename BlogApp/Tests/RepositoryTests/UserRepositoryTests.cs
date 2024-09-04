using Application.Repositories;
using Domain.Entities;
using Domain.Exeptions;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Moq;
using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace RepositoriesTests;

[ExcludeFromCodeCoverage]
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

        User user = TestHelper.User1();


        _userRepository.Register(user);
        User result = _inMemoryDb.DbContext.Users.FirstOrDefault(u => u.Email == TestHelper.User1().Email)!;

        Assert.NotNull(result);
        Assert.Equal(TestHelper.User1().UserName, result.UserName);
    }

    [Fact]
    public void Login_ShouldReturnJwtTokenForValidUser()
    {

        var user = TestHelper.User1();
        _userRepository.Register(user);


        var token = _userRepository.Login(TestHelper.User1().Email, TestHelper.User1().Password);


        Assert.NotNull(token);
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);
        Assert.Equal(TestHelper.User1().Email, jwtToken.Claims.First(c => c.Type == "Email").Value);
    }

    [Fact]
    public void Login_ShouldThrowExceptionForInvalidUser()
    {

        var exception = Assert.Throws<NotRegesterd>(() => _userRepository.Login("qais@gmail.com", "wrongpassword"));
        Assert.Equal("User with the email : qais@gmail.com not registered", exception.Message);
    }

    [Fact]
    public void Get_ShouldReturnUserForValidToken()
    {
        var user = TestHelper.User1();
        _userRepository.Register(user);

        var token = _userRepository.Login(TestHelper.User1().Email, TestHelper.User1().Password);

        var result = _userRepository.Get(token);

        Assert.NotNull(result);
        Assert.Equal(TestHelper.User1().UserName, result.UserName);
        Assert.Equal(TestHelper.User1().Email, result.Email);
    }
    [Fact]
    public void Get_ShouldThrowInvalidTokenWhenTokenIsEmpty()
    {

        string token = string.Empty;

        var exception = Assert.Throws<InvalidToken>(() => _userRepository.Get(token));
        Assert.Equal("Provided token is invalid", exception.Message);
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
    }

    [Fact]
    public void Get_ShouldThrowExceptionForInvalidTokenFormat()
    {
        var exception = Assert.Throws<InvalidToken>(() => _userRepository.Get("invalidToken"));
    }

    [Fact]
    public void Get_ShouldThrowExceptionForTokenWithMissingClaims()
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
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

        var exception = Assert.Throws<InvalidToken>(() => _userRepository.Get(tokenString));
        Assert.Equal("Token does not contain required claims", exception.Message);
    }


    [Fact]
    public void Update_ShouldUpdateUserDetails()
    {

        var user = TestHelper.User1();
        _userRepository.Register(user);

        var token = _userRepository.Login(TestHelper.User1().Email, TestHelper.User1().Password);

        var updatedUser = TestHelper.User2();


        _userRepository.Update(updatedUser, token);


        var newToken = _userRepository.Login(TestHelper.User2().Email, TestHelper.User2().Password);

        var result = _userRepository.Get(newToken);


        Assert.NotNull(result);
        Assert.Equal(TestHelper.User2().UserName, result.UserName);
        Assert.Equal(TestHelper.User2().Email, result.Email);
        Assert.Equal(TestHelper.User2().Password, result.Password);
    }
    [Fact]
    public void Update_ShouldThrowExceptionWhenTokenIsNull()
    {

        var user = TestHelper.User1();


        var exception = Assert.Throws<InvalidToken>(() => _userRepository.Update(user, null!));
    }

    [Fact]
    public void Update_ShouldThrowExceptionWhenUserNotFound()
    {

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

        var updatedUser = new User { UserName = "qaisn", Email = "qaisn@gmail.com", Password = "newpassword123" };

        var exception = Assert.Throws<UserNotFound>(() => _userRepository.Update(updatedUser, tokenString));
        Assert.Equal("There is no such user", exception.Message);
    }

    [Fact]
    public void GetByIds_ShouldReturnUsersForValidIds()
    {
        var user1 = TestHelper.User1();
        var user2 = TestHelper.User2();
        _userRepository.Register(user1);
        _userRepository.Register(user2);

        var result = _userRepository.GetByIds([1, 2]);

        Assert.Equal(2, result.Count);
        Assert.Contains(result, u => u.Id == 1);
        Assert.Contains(result, u => u.Id == 2);
    }

    [Fact]
    public void GetByIds_ShouldReturnEmptyListForInvalidIds()
    {
        var result = _userRepository.GetByIds([999]);

        Assert.Empty(result);
    }

}
