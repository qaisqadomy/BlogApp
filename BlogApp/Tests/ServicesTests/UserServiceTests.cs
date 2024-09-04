

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
        User user = TestHelper.User1();

        mockUserRepository.Setup(repo => repo.Get(token)).Returns(user);

        GetUserDTO result = userService.Get(token);

        Assert.NotNull(result);
        Assert.Equal(TestHelper.User1().UserName, result.UserName);
        Assert.Equal(TestHelper.User1().Email, result.Email);
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

        UserDTO userDto = TestHelper.UserDto();

        userService.Register(userDto);

        mockUserRepository.Verify(repo => repo.Register(It.Is<User>(u =>
            u.UserName == TestHelper.UserDto().UserName &&
            u.Email == TestHelper.UserDto().Email &&
            u.Password == TestHelper.UserDto().Password)), Times.Once);
    }

    [Fact]
    public void Update_CallsUpdateOnRepository()
    {
        UserDTO userDto = TestHelper.UserDto();
        var token = "validToken";
        userService.Update(userDto, token);
        mockUserRepository.Verify(repo => repo.Update(It.Is<User>(u =>
            u.UserName == TestHelper.UserDto().UserName &&
            u.Email == TestHelper.UserDto().Email &&
            u.Password == TestHelper.UserDto().Password), token), Times.Once);
    }

}
