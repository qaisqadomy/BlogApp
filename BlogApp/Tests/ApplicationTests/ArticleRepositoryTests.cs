using Moq;
using Xunit;
using Application.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Domain.Exeptions;

namespace Application.Tests.Repositories
{
    public class ArticleRepositoryTests
    {
        private readonly Mock<AppDbContext> _mockContext;
        private readonly Mock<DbSet<Article>> _mockArticleSet;
        private readonly ArticleRepository _repository;

        public ArticleRepositoryTests()
        {
            _mockContext = new Mock<AppDbContext>();
            _mockArticleSet = new Mock<DbSet<Article>>();
            _repository = new ArticleRepository(_mockContext.Object);

            _mockContext.Setup(m => m.Articles).Returns(_mockArticleSet.Object);
        }

        [Fact]
        public void AddArticle_ShouldAddArticle()
        {

            var article = new Article { Title = "Test Article", Slug = "qais", Description = "qqqqqq", Body = "sasasa", Tags = ["dss"], AuthorId = 0 };

            _mockArticleSet.Setup(m => m.Add(article)).Verifiable();
            _mockContext.Setup(m => m.SaveChanges()).Verifiable();


            _repository.AddArticle(article);


            _mockArticleSet.Verify(m => m.Add(article), Times.Once);
            _mockContext.Verify(m => m.SaveChanges(), Times.Once);
        }

        [Fact]
        public void GetAll_ShouldReturnAllArticles()
        {

            var articles = new List<Article>
            {
                new Article {  Title = "Test Article 1",Slug = "qais" ,Description="qqqqqq",Body = "sasasa",Tags=["dss"],AuthorId=0 },
                new Article { Title = "Test Article 2",Slug = "qais" ,Description="qqqqqq",Body = "sasasa",Tags=["dss"],AuthorId=0 }
            }.AsQueryable();

            _mockArticleSet.As<IQueryable<Article>>().Setup(m => m.Provider).Returns(articles.Provider);
            _mockArticleSet.As<IQueryable<Article>>().Setup(m => m.Expression).Returns(articles.Expression);
            _mockArticleSet.As<IQueryable<Article>>().Setup(m => m.ElementType).Returns(articles.ElementType);
            _mockArticleSet.As<IQueryable<Article>>().Setup(m => m.GetEnumerator()).Returns(articles.GetEnumerator());


            var result = _repository.GetAll();


            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void GetArticle_WithAuthor_ShouldReturnFilteredArticles()
        {

            var authorName = "Author";
            var user = new User {  UserName = authorName, Email = "Author@gmail.com", Password = "Author123" };
            var articles = new List<Article>
            {
                new Article { Title = "Test Article 1", AuthorId = 1,Slug = "qais" ,Description="qqqqqq",Body = "sasasa",Tags=["dss"]},
                new Article { Title = "Test Article 2", AuthorId = 2,Slug = "qais" ,Description="qqqqqq",Body = "sasasa",Tags=["dss"]}
            }.AsQueryable();

            _mockContext.Setup(m => m.Users.FirstOrDefault(u => u.UserName == authorName)).Returns(user);
            _mockArticleSet.As<IQueryable<Article>>().Setup(m => m.Provider).Returns(articles.Provider);
            _mockArticleSet.As<IQueryable<Article>>().Setup(m => m.Expression).Returns(articles.Expression);
            _mockArticleSet.As<IQueryable<Article>>().Setup(m => m.ElementType).Returns(articles.ElementType);
            _mockArticleSet.As<IQueryable<Article>>().Setup(m => m.GetEnumerator()).Returns(articles.GetEnumerator());


            var result = _repository.GetArticle(null, authorName, null);


            Assert.Single(result);
            Assert.Equal(1, result.First().Id);
        }

        [Fact]
        public void UpdateArticle_ShouldUpdateArticle()
        {

            var existingArticle = new Article {  Title = "Old Title" ,Slug = "qais" ,Description="qqqqqq",Body = "sasasa",Tags=["dss"],AuthorId=0};
            var updatedArticle = new Article { Title = "New Title",Slug = "qais" ,Description="qqqqqq",Body = "sasasa",Tags=["dss"],AuthorId=0};

            _mockArticleSet.Setup(m => m.FirstOrDefault(a => a.Id == 1)).Returns(existingArticle);
            _mockArticleSet.Setup(m => m.Update(existingArticle)).Verifiable();
            _mockContext.Setup(m => m.SaveChanges()).Verifiable();


            _repository.UpdateArticle(updatedArticle, 1);


            Assert.Equal("New Title", existingArticle.Title);
            _mockArticleSet.Verify(m => m.Update(existingArticle), Times.Once);
            _mockContext.Verify(m => m.SaveChanges(), Times.Once);
        }

        [Fact]
        public void UpdateArticle_ShouldThrowArticleNotFound_WhenArticleDoesNotExist()
        {

            var updatedArticle = new Article { Title = "New Title",Slug = "qais" ,Description="qqqqqq",Body = "sasasa",Tags=["dss"],AuthorId=0 };

            _mockArticleSet.Setup(m => m.FirstOrDefault(a => a.Id == 1)).Returns((Article)null!);


            Assert.Throws<ArticleNotFound>(() => _repository.UpdateArticle(updatedArticle, 1));
        }

        [Fact]
        public void DeleteArticle_ShouldDeleteArticle()
        {

            var existingArticle = new Article { Title = "New Title",Slug = "qais" ,Description="qqqqqq",Body = "sasasa",Tags=["dss"],AuthorId=0 };

            _mockArticleSet.Setup(m => m.FirstOrDefault(a => a.Id == 1)).Returns(existingArticle);
            _mockArticleSet.Setup(m => m.Remove(existingArticle)).Verifiable();
            _mockContext.Setup(m => m.SaveChanges()).Verifiable();


            _repository.DeleteArticle(1);


            _mockArticleSet.Verify(m => m.Remove(existingArticle), Times.Once);
            _mockContext.Verify(m => m.SaveChanges(), Times.Once);
        }

        [Fact]
        public void DeleteArticle_ShouldThrowArticleNotFound_WhenArticleDoesNotExist()
        {

            _mockArticleSet.Setup(m => m.FirstOrDefault(a => a.Id == 1)).Returns((Article)null!);


            Assert.Throws<ArticleNotFound>(() => _repository.DeleteArticle(1));
        }
    }
}
