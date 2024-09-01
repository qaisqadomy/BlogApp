using FluentValidation.TestHelper;
using Presentation.Validators;
using Application.DTOs;

namespace Presentation.Tests
{
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
            var model = new CommentDTO { Body = string.Empty,CreatedAt=DateTime.Now, AuthorId=1,UpdatedAt=DateTime.Now };
            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.Body)
                  .WithErrorMessage("Comment body is required.");
        }

        [Fact]
        public void Should_Have_Error_When_Body_Is_Too_Long()
        {
            var model = new CommentDTO { Body = new string('a', 1001),CreatedAt=DateTime.Now, AuthorId=1,UpdatedAt=DateTime.Now };
            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.Body)
                  .WithErrorMessage("Comment body must not exceed 1000 characters.");
        }

        [Fact]
        public void Should_Have_Error_When_CreatedAt_Is_Future()
        {
            var model = new CommentDTO { CreatedAt = DateTime.Now.AddMinutes(1) ,Body = "dsdsdsd", AuthorId=1,UpdatedAt=DateTime.Now };
            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.CreatedAt)
                  .WithErrorMessage("Creation date cannot be in the future.");
        }

        [Fact]
        public void Should_Have_Error_When_UpdatedAt_Is_Before_CreatedAt()
        {
            var model = new CommentDTO
            {
                CreatedAt = DateTime.Now.AddMinutes(-1),
                UpdatedAt = DateTime.Now.AddMinutes(-2),
                Body = "sadsad", AuthorId=1
            };
            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.UpdatedAt)
                  .WithErrorMessage("Update date must be greater than or equal to creation date.");
        }

        [Fact]
        public void Should_Have_Error_When_UpdatedAt_Is_Future()
        {
            var model = new CommentDTO
            {
                CreatedAt = DateTime.Now.AddMinutes(-1),
                UpdatedAt = DateTime.Now.AddMinutes(1),
                 Body = "sadsad", AuthorId=1
            };
            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.UpdatedAt)
                  .WithErrorMessage("Update date cannot be in the future.");
        }

        [Fact]
        public void Should_Have_Error_When_AuthorId_Is_Zero()
        {
            var model = new CommentDTO { CreatedAt = DateTime.Now.AddMinutes(1) ,Body = "dsdsdsd", AuthorId=0,UpdatedAt=DateTime.Now};
            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.AuthorId)
                  .WithErrorMessage("AuthorId must be a positive integer.");
        }

        [Fact]
        public void Should_Have_Error_When_AuthorId_Is_Negative()
        {
            var model = new CommentDTO { AuthorId = -1 ,CreatedAt = DateTime.Now.AddMinutes(1) ,Body = "dsdsdsd",UpdatedAt=DateTime.Now};
            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.AuthorId)
                  .WithErrorMessage("AuthorId must be a positive integer.");
        }

        [Fact]
        public void Should_Not_Have_Any_Validation_Errors_When_Valid()
        {
            var model = new CommentDTO
            {
                Body = "Valid comment body",
                CreatedAt = DateTime.Now.AddMinutes(-5),
                UpdatedAt = DateTime.Now.AddMinutes(-1),
                AuthorId = 1
            };
            var result = _validator.TestValidate(model);

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
