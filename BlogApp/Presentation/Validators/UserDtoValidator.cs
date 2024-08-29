using Application.DTOs;
using FluentValidation;

namespace Presentation.Validators;

public class UserDtoValidator : AbstractValidator<UserDTO>
{
    public UserDtoValidator()
    {
        RuleFor(x => x.UserName)
.NotEmpty().WithMessage("Username is required.")
.Length(3, 50).WithMessage("Username must be between 3 and 50 characters.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email address format.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");
    }
}