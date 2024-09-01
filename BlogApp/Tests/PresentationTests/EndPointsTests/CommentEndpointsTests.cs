using Microsoft.AspNetCore.Mvc.Testing;
using System.Text.Json;
using Presentation;
using Application.DTOs;
using System.Text;
using Domain.Entities;
using Moq;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;

namespace PresentationTests.EndPointsTests
{
    public class CommentEndpointsTests : IClassFixture<WebApplicationFactory<Program>>
    {
          private readonly HttpClient _client;
          private readonly InMemoryDbContext dbContext;
        public CommentEndpointsTests(WebApplicationFactory<Program> factory)
        {
            dbContext = new InMemoryDbContext();
            var webAppFactory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    
                    var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));
                    if (descriptor != null)
                    {
                        services.Remove(descriptor);
                    }
                    services.AddSingleton(dbContext.DbContext);
                });
            });

            _client = factory.CreateClient();
        }
        [Fact]
        public async Task GetAll_ShouldReturnOkWithComments()
        {

            var response = await _client.GetAsync("/comment/");


            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
            };

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
    
    }
}
