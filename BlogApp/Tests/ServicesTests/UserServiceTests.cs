

using Application.DTOs;
using Application.Services;
using Domain.Entities;
using Domain.IRepositories;
using Moq;

namespace ServicesTests;

public class UserServiceTests
{
    private readonly Mock<IUserRepository> mockUserRepository;
    private readonly UserService userService;

    public UserServiceTests()
    {
        mockUserRepository = new Mock<IUserRepository>();
        userService = new UserService(mockUserRepository.Object);
    }

    [Fact]
    public void Get_ReturnsCorrectUserDTO()
    {

        string token = "validToken";
        User user = new()
        {
            UserName = "testUser",
            Email = "testuser@example.com",
            Password = "123"

        };

        mockUserRepository.Setup(repo => repo.Get(token)).Returns(user);

        GetUserDTO result = userService.Get(token);

        Assert.NotNull(result);
        Assert.Equal("testUser", result.UserName);
        Assert.Equal("testuser@example.com", result.Email);
    }

    [Fact]
    public void Login_ReturnsToken()
    {

        string email = "testuser@example.com";
        string password = "password123";
        string token = "loginToken";

        mockUserRepository.Setup(repo => repo.Login(email, password)).Returns(token);


        string result = userService.Login(email, password);


        Assert.Equal(token, result);
    }

    [Fact]
    public void Register_CallsRegisterOnRepository()
    {

        UserDTO userDto = new()
        {
            UserName = "newUser",
            Email = "newuser@example.com",
            Password = "newPassword"
        };


        userService.Register(userDto);


        mockUserRepository.Verify(repo => repo.Register(It.Is<User>(u =>
            u.UserName == "newUser" &&
            u.Email == "newuser@example.com" &&
            u.Password == "newPassword")), Times.Once);
    }

    [Fact]
    public void Update_CallsUpdateOnRepository()
    {

        UserDTO userDto = new()
        {
            UserName = "updatedUser",
            Email = "updateduser@example.com",
            Password = "updatedPassword"
        };
        var token = "validToken";
        userService.Update(userDto, token);
        mockUserRepository.Verify(repo => repo.Update(It.Is<User>(u =>
            u.UserName == "updatedUser" &&
            u.Email == "updateduser@example.com" &&
            u.Password == "updatedPassword"), token), Times.Once);
    }

}
