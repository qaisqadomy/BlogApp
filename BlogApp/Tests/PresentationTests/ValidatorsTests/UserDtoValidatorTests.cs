using Presentation.Validators;
using Application.DTOs;

namespace PresentationTests;

public class UserDtoValidatorTests
{
    private readonly UserDtoValidator _validator;

    public UserDtoValidatorTests()
    {
        _validator = new UserDtoValidator();
    }

    [Fact]
    public void Should_Have_Error_When_UserName_Is_Empty()
    {
        var model = new UserDTO { UserName = string.Empty, Email = "DSDSDS@GMAIL.COM", Password = "112" };
        var result = _validator.Validate(model);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "UserName" && e.ErrorMessage == "Username is required.");
    }

    [Fact]
    public void Should_Have_Error_When_UserName_Is_Too_Short()
    {
        var model = new UserDTO { UserName = "ab", Email = "DSDSDS@GMAIL.COM", Password = "112" };
        var result = _validator.Validate(model);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "UserName" && e.ErrorMessage == "Username must be between 3 and 50 characters.");
    }

    [Fact]
    public void Should_Have_Error_When_UserName_Is_Too_Long()
    {
        var model = new UserDTO { UserName = new string('a', 51), Email = "DSDSDS@GMAIL.COM", Password = "112" };
        var result = _validator.Validate(model);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "UserName" && e.ErrorMessage == "Username must be between 3 and 50 characters.");
    }

    [Fact]
    public void Should_Have_Error_When_Email_Is_Empty()
    {
        var model = new UserDTO { Email = string.Empty, UserName = "QAIS", Password = "123" };
        var result = _validator.Validate(model);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Email" && e.ErrorMessage == "Email is required.");
    }

    [Fact]
    public void Should_Have_Error_When_Email_Is_Invalid()
    {
        var model = new UserDTO { Email = "invalidemail", UserName = "QAIS", Password = "123" };
        var result = _validator.Validate(model);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Email" && e.ErrorMessage == "Invalid email address format.");
    }

    [Fact]
    public void Should_Have_Error_When_Password_Is_Empty()
    {
        var model = new UserDTO { Password = string.Empty, Email = "invalidemail", UserName = "QAIS" };
        var result = _validator.Validate(model);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Password" && e.ErrorMessage == "Password is required.");
    }

    [Fact]
    public void Should_Have_Error_When_Password_Is_Too_Short()
    {
        var model = new UserDTO { Password = "12345", Email = "invalidemail", UserName = "QAIS" };
        var result = _validator.Validate(model);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Password" && e.ErrorMessage == "Password must be at least 6 characters long.");
    }

    [Fact]
    public void Should_Not_Have_Any_Validation_Errors_When_Valid()
    {
        var model = new UserDTO
        {
            UserName = "ValidUser",
            Email = "validuser@example.com",
            Password = "ValidPwd123"
        };
        var result = _validator.Validate(model);

        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }
}
