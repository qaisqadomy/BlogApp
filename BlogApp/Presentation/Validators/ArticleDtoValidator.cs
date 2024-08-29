using Application.DTOs;
using FluentValidation;

namespace Presentation.Validators;

public class ArticleDtoValidator : AbstractValidator<ArticleDTO>
{
    public ArticleDtoValidator()
    {
        RuleFor(x => x.Slug)
            .NotEmpty().WithMessage("Slug is required.")
            .Length(1, 100).WithMessage("Slug must be between 1 and 100 characters.");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .Length(1, 200).WithMessage("Title must be between 1 and 200 characters.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .Length(1, 500).WithMessage("Description must be between 1 and 500 characters.");

        RuleFor(x => x.Body)
            .NotEmpty().WithMessage("Body is required.");

        RuleFor(x => x.Tags)
            .Must(tags => tags == null || tags.Count <= 10).WithMessage("Tags can have a maximum of 10 items.");

        RuleFor(x => x.CreatedAt)
            .LessThanOrEqualTo(DateTime.Now).WithMessage("Creation date cannot be in the future.");

        RuleFor(x => x.UpdatedAt)
            .GreaterThanOrEqualTo(x => x.CreatedAt).WithMessage("Update date must be greater than or equal to the creation date.")
            .LessThanOrEqualTo(DateTime.Now).WithMessage("Update date cannot be in the future.");

        RuleFor(x => x.AuthorId)
            .GreaterThan(0).WithMessage("AuthorId must be a positive integer.");
    }
}
