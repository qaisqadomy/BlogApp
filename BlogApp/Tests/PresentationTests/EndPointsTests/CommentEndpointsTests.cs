using System.Net;
using System.Text;
using System.Text.Json;
using Application.DTOs;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Presentation;
using Domain.Entities;

namespace PresentationTests.EndPointsTests
{
    public class CommentEndpointsTests(CommentTestFixture fixture) : IClassFixture<CommentTestFixture>
    {
        private readonly HttpClient _client = fixture.Client;

        [Fact]
        public async Task GetAll_ShouldReturnOkWithComments()
        {
            var response = await _client.GetAsync("/comment/");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var comments = JsonSerializer.Deserialize<List<CommentViewDTO>>(responseString, options);

            Assert.NotNull(comments);
            Assert.NotEmpty(comments);
        }

        [Fact]
        public async Task Post_ShouldAddComment()
        {
            var newComment = new CommentDTO
            {
                Body = "This is a new comment",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                AuthorId = 1
            };

            var content = new StringContent(JsonSerializer.Serialize(newComment), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/comment/", content);

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Delete_ShouldRemoveCommentAndReturnNoContent()
        {
            var response = await _client.DeleteAsync($"/comment/{1}");
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}


public class CommentTestFixture : IDisposable
    {
        public HttpClient Client { get; private set; }
        private readonly WebApplicationFactory<Program> _factory;

        public CommentTestFixture()
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
            context.Comments.RemoveRange(context.Comments);
            context.SaveChanges();

            context.Comments.AddRange(
                new Comment { Body = "Test Comment 1", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, AuthorId = 1 },
                new Comment { Body = "Test Comment 2", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, AuthorId = 2 }
            );
            context.SaveChanges();
        }

        public void Dispose()
        {

            _factory.Dispose();


            Client?.Dispose();
        }
    }
