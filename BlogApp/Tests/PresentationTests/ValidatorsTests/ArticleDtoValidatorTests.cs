using FluentValidation.Results;
using Presentation.Validators;
using Application.DTOs;

namespace PresentationTests.ValidatorsTests;

public class ArticleDtoValidatorTests
{
    private readonly ArticleDtoValidator _validator;

    public ArticleDtoValidatorTests()
    {
        _validator = new ArticleDtoValidator();
    }

    private ValidationResult ValidateArticleDTO(ArticleDTO articleDto)
    {
        return _validator.Validate(articleDto);
    }

    [Fact]
    public void Should_Have_Error_When_Slug_Is_Empty()
    {
        ArticleDTO articleDto = TestHelper.ArticleDTO();
        articleDto.Slug = "";

        ValidationResult result = ValidateArticleDTO(articleDto);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Slug" && e.ErrorMessage == "Slug is required.");
    }

    [Fact]
    public void Should_Have_Error_When_Slug_Is_Too_Long()
    {

        ArticleDTO articleDto = TestHelper.ArticleDTO();
        articleDto.Slug = new string('a', 101);

        ValidationResult result = ValidateArticleDTO(articleDto);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Slug" && e.ErrorMessage == "Slug must be between 1 and 100 characters.");
    }


    [Fact]
    public void Should_Have_Error_When_Title_Is_Empty()
    {
         ArticleDTO articleDto = TestHelper.ArticleDTO();
         articleDto.Title="";
        
        ValidationResult result = ValidateArticleDTO(articleDto);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Title" && e.ErrorMessage == "Title is required.");
    }

    [Fact]
    public void Should_Have_Error_When_Title_Is_Too_Long()
    {
        ArticleDTO articleDto = TestHelper.ArticleDTO();
         articleDto.Title=new string('a', 201);
        

        ValidationResult result = ValidateArticleDTO(articleDto);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Title" && e.ErrorMessage == "Title must be between 1 and 200 characters.");
    }


    [Fact]
    public void Should_Have_Error_When_Description_Is_Empty()
    {
        ArticleDTO articleDto = TestHelper.ArticleDTO();
         articleDto.Description="";
       

        ValidationResult result = ValidateArticleDTO(articleDto);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Description" && e.ErrorMessage == "Description is required.");
    }

    [Fact]
    public void Should_Have_Error_When_Description_Is_Too_Long()
    {
        ArticleDTO articleDto = TestHelper.ArticleDTO();
         articleDto.Description=new string('a', 501);

        ValidationResult result = ValidateArticleDTO(articleDto);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Description" && e.ErrorMessage == "Description must be between 1 and 500 characters.");
    }

    [Fact]
    public void Should_Have_Error_When_Body_Is_Empty()
    {
        ArticleDTO articleDto = TestHelper.ArticleDTO();
         articleDto.Body="";

        ValidationResult result = ValidateArticleDTO(articleDto);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Body" && e.ErrorMessage == "Body is required.");
    }

    [Fact]
    public void Should_Have_Error_When_Tags_Exceed_Max_Count()
    {
        ArticleDTO articleDto = TestHelper.ArticleDTO();
         articleDto.Tags=new List<string> { "tag1", "tag2", "tag3", "tag4", "tag5", "tag6", "tag7", "tag8", "tag9", "tag10", "tag11" };

        ValidationResult result = ValidateArticleDTO(articleDto);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Tags" && e.ErrorMessage == "Tags can have a maximum of 10 items.");
    }

    [Fact]
    public void Should_Have_Error_When_CreatedAt_Is_In_Future()
    {
         ArticleDTO articleDto = TestHelper.ArticleDTO();
         articleDto.CreatedAt=DateTime.Now.AddDays(1);

        ValidationResult result = ValidateArticleDTO(articleDto);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "CreatedAt" && e.ErrorMessage == "Creation date cannot be in the future.");
    }

    [Fact]
    public void Should_Have_Error_When_UpdatedAt_Is_Before_CreatedAt()
    {
         ArticleDTO articleDto = TestHelper.ArticleDTO();
         articleDto.UpdatedAt=DateTime.Now.AddDays(-1);

        ValidationResult result = ValidateArticleDTO(articleDto);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "UpdatedAt" && e.ErrorMessage == "Update date must be greater than or equal to the creation date.");
    }

    [Fact]
    public void Should_Have_Error_When_UpdatedAt_Is_In_Future()
    {
            ArticleDTO articleDto = TestHelper.ArticleDTO();
         articleDto.UpdatedAt=DateTime.Now.AddDays(1);

        var result = ValidateArticleDTO(articleDto);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "UpdatedAt" && e.ErrorMessage == "Update date cannot be in the future.");
    }

    [Fact]
    public void Should_Have_Error_When_AuthorId_Is_Not_Positive()
    {
            ArticleDTO articleDto = TestHelper.ArticleDTO();
         articleDto.AuthorId=-1;

        var result = ValidateArticleDTO(articleDto);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "AuthorId" && e.ErrorMessage == "AuthorId must be a positive integer.");
    }

}
