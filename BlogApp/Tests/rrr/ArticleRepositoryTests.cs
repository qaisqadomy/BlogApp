using Application.Repositories;
using Domain.Entities;
using Domain.Exeptions;
using Microsoft.Extensions.Configuration;
using Moq;

namespace RepositoriesTests
{
    public class ArticleRepositoryTests
    {
        private readonly InMemoryDB _inMemoryDb;
        private readonly ArticleRepository _repository;
        private readonly UserRepository userRepository;

        public ArticleRepositoryTests()
        {
            _inMemoryDb = new InMemoryDB();
            _repository = new ArticleRepository(_inMemoryDb.DbContext);
             var mockConfiguration = new Mock<IConfiguration>();
            userRepository = new UserRepository(_inMemoryDb.DbContext, mockConfiguration.Object);
        }

        [Fact]
        public void AddArticle_ShouldAddArticle()
        {
            var article = new Article { Title = "New Test Article", Slug = "qais", Description = "qqqqqq", Body = "sasasa", Tags = ["dss"], AuthorId = 0 };

            _repository.AddArticle(article);
            

            var addedArticle = _inMemoryDb.DbContext.Articles.Find(1);
            Assert.NotNull(addedArticle);
            Assert.Equal("New Test Article", addedArticle.Title);
        }

        [Fact]
        public void GetAll_ShouldReturnAllArticles()
        {
            var article = new Article { Title = "New Test Article", Slug = "qais", Description = "qqqqqq", Body = "sasasa", Tags = ["dss"], AuthorId = 0 };

            _repository.AddArticle(article);

            var result = _repository.GetAll();

            Assert.Single(result);
        }

        [Fact]
        public void UpdateArticle_ShouldUpdateArticle()
        {
            var article = new Article
            {
                Title = "Updated Title",
                Slug = "qais",
                Description = "qqqqqq",
                Body = "sasasa",
                Tags = ["dss"],
                AuthorId = 0
            };
            _repository.AddArticle(article);
            _repository.UpdateArticle(article, 1);

            var updatedArticle = _inMemoryDb.DbContext.Articles.Find(1);
            Assert.Equal("Updated Title", updatedArticle!.Title);
        }

        [Fact]
        public void DeleteArticle_ShouldDeleteArticle()
        {
            var article = new Article { Title = "Title", Slug = "qais", Description = "qqqqqq", Body = "sasasa", Tags = ["dss"], AuthorId = 0 };
            _inMemoryDb.DbContext.Articles.Add(article);
            _inMemoryDb.DbContext.SaveChanges();

            _repository.DeleteArticle(1);

            var deletedArticle = _inMemoryDb.DbContext.Articles.Find(1);
            Assert.Null(deletedArticle);
        }

        [Fact]
        public void UpdateArticle_ShouldThrowArticleNotFound_WhenArticleDoesNotExist()
        {
            var article = new Article { Title = "Title", Slug = "qais", Description = "qqqqqq", Body = "sasasa", Tags = ["dss"], AuthorId = 0 };
            Assert.Throws<ArticleNotFound>(() => _repository.UpdateArticle(article, 1));
        }

        [Fact]
        public void DeleteArticle_ShouldThrowArticleNotFound_WhenArticleDoesNotExist()
        {
            Assert.Throws<ArticleNotFound>(() => _repository.DeleteArticle(1));
        }
        [Fact]
        public void GetArticle_NoFilters_ReturnsAllArticles()
        {
            
            var result = _repository.GetArticle(null, null, null);

            
            Assert.Empty(result);
        }

        [Fact]
        public void GetArticle_NonExistentAuthor_ReturnsEmptyList()
        {
            
            var result = _repository.GetArticle(null, "nonexistent", null);

            
            Assert.Empty(result);
        }

    }
}

