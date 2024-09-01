using System.Net;
using System.Text;
using System.Text.Json;
using Application.DTOs;
using Domain.Entities;
using Xunit;
using Xunit.Abstractions;

namespace PresentationTests.EndPointsTests
{
    public class ArticleEndpointsTests : IClassFixture<ArticleTestFixture>
    {
        private readonly HttpClient _client;

        public ArticleEndpointsTests(ArticleTestFixture fixture, ITestOutputHelper output)
        {
            _client = fixture.Client;
        }

        [Fact]
        public async Task GetAll_ShouldReturnOkWithArticles()
        {
            var response = await _client.GetAsync("/article/articles");

            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var articles = JsonSerializer.Deserialize<List<ArticleViewDTO>>(responseString, options);

            Assert.NotNull(articles);
            Assert.NotEmpty(articles);
        }

        [Fact]
        public async Task GetArticle_WithParameters_ShouldReturnOkWithArticles()
        {
            var response = await _client.GetAsync("/article?tag=Tag1");

            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var articles = JsonSerializer.Deserialize<List<ArticleViewDTO>>(responseString, options);

            Assert.NotNull(articles);
        }

        [Fact]
        public async Task Post_ShouldAddArticle()
        {
            var newArticle = new Article
            {
                Slug = "NEW_SLUG",
                Description = "New Description",
                Title = "New Article",
                Body = "Content for new article.",
                AuthorId = 3,
                Tags = new List<string> { "NewTag" },
                Favorited = false,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            var content = new StringContent(JsonSerializer.Serialize(newArticle), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/article/", content);

            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var createdArticle = JsonSerializer.Deserialize<ArticleDTO>(responseString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            Assert.NotNull(createdArticle);
            Assert.Equal(newArticle.Title, createdArticle.Title);
        }

        [Fact]
        public async Task Put_ShouldUpdateArticle()
        {
            var articleId = 1;
            var updatedArticle = new Article
            {
                Slug = "UPDATED_SLUG",
                Description = "Updated Description",
                Title = "Updated Article",
                Body = "Updated content.",
                AuthorId = 1,
                Tags = new List<string> { "UpdatedTag" },
                Favorited = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            var content = new StringContent(JsonSerializer.Serialize(updatedArticle), Encoding.UTF8, "application/json");

            var response = await _client.PutAsync($"/article/{articleId}", content);

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Delete_ShouldRemoveArticleAndReturnNoContent()
        {
            var articleId = 2;

            var response = await _client.DeleteAsync($"/article/{articleId}");

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}
