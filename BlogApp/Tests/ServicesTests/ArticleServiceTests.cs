using Moq;
using Application.Services;
using Domain.IRepositories;
using Domain.Entities;
using Application.DTOs;


namespace ServicesTests;

public class ArticleServiceTests
{
    private readonly Mock<IArticleRepository> mockArticleRepository;
    private readonly Mock<IUserRepository> mockUserRepository;
        private readonly ArticleService articleService;

        public ArticleServiceTests()
        {
            mockArticleRepository = new Mock<IArticleRepository>();
            mockUserRepository = new Mock<IUserRepository>();
            articleService = new ArticleService(mockArticleRepository.Object, mockUserRepository.Object);
        }

        [Fact]
        public void GetAll_ReturnsArticleViewDTOs()
        {
            
            List<Article> articles =
            [
                new Article { Slug = "slug1", Title = "Title1", Description = "Desc1", Body = "Body1", Tags =["tag1"] , CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now, Favorited = false, FavoritesCount = 0, AuthorId = 1 },
                new Article { Slug = "slug2", Title = "Title2", Description = "Desc2", Body = "Body2", Tags =["tag2"] , CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now, Favorited = true, FavoritesCount = 10, AuthorId = 2 }
            ];
            List<User> users =
            [
                new User { Id = 1, UserName = "User1", Email = "user1@example.com",Password="123", Bio = "Bio1", Image = "Image1", Following = true },
                new User { Id = 2, UserName = "User2", Email = "user2@example.com",Password="123", Bio = "Bio2", Image = "Image2", Following = false }
            ];

            mockArticleRepository.Setup(repo => repo.GetAll()).Returns(articles);
            mockUserRepository.Setup(repo => repo.GetByIds(It.IsAny<List<int>>())).Returns(users);

            
            var result = articleService.GetAll();

            
            Assert.Equal(2, result.Count);
            Assert.Equal("Title1", result[0].Title);
            Assert.Equal("Title2", result[1].Title);
            Assert.Equal("User1", result[0].UserDataDTO.UserName);
            Assert.Equal("User2", result[1].UserDataDTO.UserName);
        }

        [Fact]
        public void GetArticle_ReturnsFilteredArticleViewDTOs()
        {
            
            List<Article> articles =
            [
                new() { Slug = "slug1", Title = "Title1", Description = "Desc1", Body = "Body1", Tags =["tag1"] , CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now, Favorited = false, FavoritesCount = 0, AuthorId = 1 }
            ];
            List<User> users =
            [
                new() { Id = 1, UserName = "User1", Email = "user1@example.com", Password="123",Bio = "Bio1", Image = "Image1", Following = true }
            ];

            mockArticleRepository.Setup(repo => repo.GetArticle(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool?>())).Returns(articles);
            mockUserRepository.Setup(repo => repo.GetByIds(It.IsAny<List<int>>())).Returns(users);

            
            var result = articleService.GetArticle(null, null, null);

            
            Assert.Single(result);
            Assert.Equal("Title1", result[0].Title);
            Assert.Equal("User1", result[0].UserDataDTO.UserName);
        }

        [Fact]
        public void AddArticle_CallsAddArticleOnRepository()
        {
            
            ArticleDTO articleDto = new ArticleDTO
            {
                Slug = "slug1",
                Title = "Title1",
                Description = "Desc1",
                Body = "Body1",
                Tags = ["tag1"],
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Favorited = false,
                FavoritesCount = 0,
                AuthorId = 1
            };

            
            articleService.AddArticle(articleDto);

            
            mockArticleRepository.Verify(repo => repo.AddArticle(It.IsAny<Article>()), Times.Once);
        }

        [Fact]
        public void UpdateArticle_CallsUpdateArticleOnRepository()
        {
            ArticleDTO articleDto = new ArticleDTO
            {
                Slug = "slug1",
                Title = "Title1",
                Description = "Desc1",
                Body = "Body1",
                Tags = ["tag1"],
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Favorited = false,
                FavoritesCount = 0,
                AuthorId = 1
            };
            int articleId = 1;

            articleService.UpdateArticle(articleDto, articleId);

            
            mockArticleRepository.Verify(repo => repo.UpdateArticle(It.IsAny<Article>(), articleId), Times.Once);
        }

        [Fact]
        public void DeleteArticle_CallsDeleteArticleOnRepository()
        {
            
            int articleId = 1;

            
            articleService.DeleteArticle(articleId);

            
            mockArticleRepository.Verify(repo => repo.DeleteArticle(articleId), Times.Once);
        }
    }

