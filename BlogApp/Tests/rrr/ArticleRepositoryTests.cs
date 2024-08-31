using Application.Repositories;
using Domain.Entities;
using Domain.Exeptions;


namespace RepositoriesTests;

public class ArticleRepositoryTests
{

    private readonly InMemoryDB _inMemoryDb;
    private readonly ArticleRepository _articleRepository;

    public ArticleRepositoryTests()
    {
        _inMemoryDb = new InMemoryDB();
        _articleRepository = new ArticleRepository(_inMemoryDb.DbContext);
    }

    [Fact]
    public void AddArticle_ShouldAddArticleToDatabase()
    {
        // Arrange
        var article = new Article { Title = "Title", Slug = "qais", Description = "qqqqqq", Body = "sasasa", Tags = ["dss"], AuthorId = 0 };

        // Act
        _articleRepository.AddArticle(article);
        var result = _articleRepository.GetAll();

        // Assert
        Assert.Single(result);
        Assert.Equal("Title", result.First().Title);
    }

    [Fact]
    public void GetAll_ShouldReturnAllArticles()
    {
        // Arrange
        var article1 = new Article { Title = "Title1", Slug = "qais", Description = "qqqqqq", Body = "sasasa", Tags = ["dss"], AuthorId = 0 };
        var article2 = new Article { Title = "Title2", Slug = "qais", Description = "qqqqqq", Body = "sasasa", Tags = ["dss"], AuthorId = 0 };
        _articleRepository.AddArticle(article1);
        _articleRepository.AddArticle(article2);

        // Act
        var result = _articleRepository.GetAll();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.Contains(result, a => a.Title == "Title1");
        Assert.Contains(result, a => a.Title == "Title2");
    }

    [Fact]
    public void GetArticle_ShouldFilterByTagAuthorFavorited()
    {
        // Arrange
        var user = new User { Id = 1, UserName = "qais", Email = "qais@gmail.com", Password = "123" };
        _inMemoryDb.DbContext.Users.Add(user);

        var article1 = new Article { Id = 1, Title = "Article 1", Tags = ["Tech"], AuthorId = 1, Favorited = true, Slug = "qais", Description = "qais", Body = "dsds" };
        var article2 = new Article { Id = 2, Title = "Article 2", Tags = ["Science"], AuthorId = 1, Favorited = false, Slug = "qais", Description = "qais", Body = "dsds" };
        _articleRepository.AddArticle(article1);
        _articleRepository.AddArticle(article2);

        var result = _articleRepository.GetArticle("Tech", "qais", true);

        Assert.Single(result);
        Assert.Equal("Article 1", result.First().Title);
    }

    [Fact]
    public void UpdateArticle_ShouldUpdateExistingArticle()
    {
        // Arrange
        var article = new Article { Title = "Title", Slug = "qais", Description = "qqqqqq", Body = "sasasa", Tags = ["dss"], AuthorId = 0 };
        _articleRepository.AddArticle(article);

        var updatedArticle = new Article { Title = "Updated Title", Slug = "qais", Description = "Updated description", Body = "sasasa", Tags = ["dss"], AuthorId = 0 };

        // Act
        _articleRepository.UpdateArticle(updatedArticle, 1);
        var result = _articleRepository.GetAll().FirstOrDefault(a => a.Id == 1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Updated Title", result.Title);
        Assert.Equal("Updated description", result.Description);
    }

    [Fact]
    public void UpdateArticle_ShouldThrowExceptionWhenArticleNotFound()
    {
        // Arrange
        var updatedArticle = new Article { Title = "Title", Slug = "qais", Description = "qqqqqq", Body = "sasasa", Tags = ["dss"], AuthorId = 0 };

        // Act & Assert
        var exception = Assert.Throws<ArticleNotFound>(() => _articleRepository.UpdateArticle(updatedArticle, 999));
        Assert.Equal("article with the Id : 999 not found", exception.Message);
    }

    [Fact]
    public void DeleteArticle_ShouldRemoveArticleFromDatabase()
    {
        // Arrange
        var article = new Article { Title = "Title", Slug = "qais", Description = "qqqqqq", Body = "sasasa", Tags = ["dss"], AuthorId = 0 };
        _articleRepository.AddArticle(article);

        // Act
        _articleRepository.DeleteArticle(1);
        var result = _articleRepository.GetAll();

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void DeleteArticle_ShouldThrowExceptionWhenArticleNotFound()
    {
        // Act & Assert
        var exception = Assert.Throws<ArticleNotFound>(() => _articleRepository.DeleteArticle(999));
        Assert.Equal("article with the Id : 999 not found", exception.Message);
    }

    public void Dispose()
    {
        _inMemoryDb.Dispose();
    }
}

