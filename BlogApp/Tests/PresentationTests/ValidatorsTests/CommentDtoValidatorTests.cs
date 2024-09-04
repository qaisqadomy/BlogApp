using FluentValidation.TestHelper;
using Presentation.Validators;
using Application.DTOs;
using PresentationTests;

namespace Presentation.Tests;

public class CommentDtoValidatorTests
{
    private readonly CommentDtoValidator _validator;

    public CommentDtoValidatorTests()
    {
        _validator = new CommentDtoValidator();
    }

    [Fact]
    public void Should_Have_Error_When_Body_Is_Empty()
    {
        CommentDTO model = TestHelper.CommentDto();
        model.Body=string.Empty;        

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.Body)
              .WithErrorMessage("Comment body is required.");
    }

    [Fact]
    public void Should_Have_Error_When_Body_Is_Too_Long()
    {
        CommentDTO model = TestHelper.CommentDto();
         model.Body= new string('a', 1001);

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.Body)
              .WithErrorMessage("Comment body must not exceed 1000 characters.");
    }

    [Fact]
    public void Should_Have_Error_When_CreatedAt_Is_Future()
    {
         CommentDTO model = TestHelper.CommentDto();
         model.CreatedAt= DateTime.Now.AddDays(1);

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.CreatedAt)
              .WithErrorMessage("Creation date cannot be in the future.");
    }

    [Fact]
    public void Should_Have_Error_When_UpdatedAt_Is_Before_CreatedAt()
    {
        
           CommentDTO model = TestHelper.CommentDto();
         model.UpdatedAt= DateTime.Now.AddDays(-1);
        
        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.UpdatedAt)
              .WithErrorMessage("Update date must be greater than or equal to creation date.");
    }

    [Fact]
    public void Should_Have_Error_When_UpdatedAt_Is_Future()
    {
         CommentDTO model = TestHelper.CommentDto();
         model.UpdatedAt= DateTime.Now.AddDays(1);
        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.UpdatedAt)
              .WithErrorMessage("Update date cannot be in the future.");
    }

    [Fact]
    public void Should_Have_Error_When_AuthorId_Is_Zero()
    {
         CommentDTO model = TestHelper.CommentDto();
         model.AuthorId= 0;

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.AuthorId)
              .WithErrorMessage("AuthorId must be a positive integer.");
    }

    [Fact]
    public void Should_Have_Error_When_AuthorId_Is_Negative()
    {
        CommentDTO model = TestHelper.CommentDto();
         model.AuthorId= -2;
        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.AuthorId)
              .WithErrorMessage("AuthorId must be a positive integer.");
    }

    [Fact]
    public void Should_Not_Have_Any_Validation_Errors_When_Valid()
    {
        CommentDTO model = new CommentDTO
        {
            Body = "Valid comment body",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            AuthorId = 1
        };
        var result = _validator.TestValidate(model);

        result.ShouldNotHaveAnyValidationErrors();
    }
}
