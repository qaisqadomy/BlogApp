using Presentation.Validators;
using Application.DTOs;

namespace PresentationTests.ValidatorsTests;

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

        UserDTO model = TestHelper.UserDto();
        model.UserName = "";
        var result = _validator.Validate(model);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "UserName" && e.ErrorMessage == "Username is required.");
    }

    [Fact]
    public void Should_Have_Error_When_UserName_Is_Too_Short()
    {
        UserDTO model = TestHelper.UserDto();
        model.UserName = "ab";
        var result = _validator.Validate(model);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "UserName" && e.ErrorMessage == "Username must be between 3 and 50 characters.");
    }

    [Fact]
    public void Should_Have_Error_When_UserName_Is_Too_Long()
    {
        UserDTO model = TestHelper.UserDto();
        model.UserName = new string('a', 51);
        var result = _validator.Validate(model);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "UserName" && e.ErrorMessage == "Username must be between 3 and 50 characters.");
    }

    [Fact]
    public void Should_Have_Error_When_Email_Is_Empty()
    {
        UserDTO model = TestHelper.UserDto();
        model.Email = string.Empty;
        var result = _validator.Validate(model);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Email" && e.ErrorMessage == "Email is required.");
    }

    [Fact]
    public void Should_Have_Error_When_Email_Is_Invalid()
    {
        UserDTO model = TestHelper.UserDto();
        model.Email = "invalidemail";
        var result = _validator.Validate(model);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Email" && e.ErrorMessage == "Invalid email address format.");
    }

    [Fact]
    public void Should_Have_Error_When_Password_Is_Empty()
    {
        UserDTO model = TestHelper.UserDto();
        model.Password = string.Empty;

        var result = _validator.Validate(model);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Password" && e.ErrorMessage == "Password is required.");
    }

    [Fact]
    public void Should_Have_Error_When_Password_Is_Too_Short()
    {
        UserDTO model = TestHelper.UserDto();
        model.Password = "123";

        var result = _validator.Validate(model);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Password" && e.ErrorMessage == "Password must be at least 6 characters long.");
    }

    [Fact]
    public void Should_Not_Have_Any_Validation_Errors_When_Valid()
    {
        UserDTO model = TestHelper.UserDto();
        var result = _validator.Validate(model);

        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }
}
