using System.Net;
using System.Text;
using System.Text.Json;
using Application.DTOs;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Presentation;
using PresentationTests;

namespace PresentationTests.EndPointsTests
{
    public class ArticleEndpointsTests(ArticleTestFixture fixture ) : IClassFixture<ArticleTestFixture>
    {
        private readonly HttpClient _client = fixture.Client;

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
            var response = await _client.GetAsync("/article?tag=tag1");

            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var articles = JsonSerializer.Deserialize<List<ArticleViewDTO>>(responseString, options);

            Assert.NotNull(articles);
        }

        [Fact]
        public async Task Post_ShouldAddArticle()
        {
            var newArticle =TestHelper.Article3();

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
            var updatedArticle = TestHelper.Article2();

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

 public class ArticleTestFixture : IDisposable
    {
        public HttpClient Client { get; private set; }
        private readonly WebApplicationFactory<Program> _factory;

        public ArticleTestFixture()
        {
            _factory = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        var descriptor = services.SingleOrDefault(
                            d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));

                        if (descriptor != null)
                        {
                            services.Remove(descriptor);
                        }

                        services.AddDbContext<AppDbContext>(options =>
                        {
                            options.UseInMemoryDatabase("InMemoryDbForTesting");
                        });

                        var serviceProvider = services.BuildServiceProvider();

                        using (var scope = serviceProvider.CreateScope())
                        {
                            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                            db.Database.EnsureCreated();
                            SeedDatabase(db);
                        }
                    });
                });

            Client = _factory.CreateClient();
        }

        private static void SeedDatabase(AppDbContext context)
        {
            context.Articles.RemoveRange(context.Articles);
            context.SaveChanges();

            var articles = new List<Article>
            {
                TestHelper.Article1(),
               TestHelper.Article2()
            };

            context.Articles.AddRange(articles);
            context.SaveChanges();
        }

        public void Dispose()
        {

            _factory.Dispose();


            Client?.Dispose();
        }
    }
