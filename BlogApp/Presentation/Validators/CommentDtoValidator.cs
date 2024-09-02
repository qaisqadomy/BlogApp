using Application.DTOs;
using FluentValidation;

namespace Presentation.Validators;

/// <summary>
/// Validator for <see cref="CommentDTO"/> using FluentValidation.
/// </summary>
public class CommentDtoValidator : AbstractValidator<CommentDTO>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CommentDtoValidator"/> class.
    /// </summary>
    public CommentDtoValidator()
    {
        RuleFor(x => x.Body)
            .NotEmpty().WithMessage("Comment body is required.")
            .MaximumLength(1000).WithMessage("Comment body must not exceed 1000 characters.");

        RuleFor(x => x.CreatedAt)
            .NotEmpty().WithMessage("Creation date is required.")
            .LessThanOrEqualTo(DateTime.Now).WithMessage("Creation date cannot be in the future.");

        RuleFor(x => x.UpdatedAt)
            .NotEmpty().WithMessage("Update date is required.")
            .GreaterThanOrEqualTo(x => x.CreatedAt).WithMessage("Update date must be greater than or equal to creation date.")
            .LessThanOrEqualTo(DateTime.Now).WithMessage("Update date cannot be in the future.");

        RuleFor(x => x.AuthorId)
            .GreaterThan(0).WithMessage("AuthorId must be a positive integer.");
    }
}
