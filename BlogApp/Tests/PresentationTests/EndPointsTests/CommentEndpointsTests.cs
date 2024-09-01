using System.Net;
using System.Text;
using System.Text.Json;
using Application.DTOs;
using Microsoft.AspNetCore.Mvc.Testing;
using Presentation;
using Xunit;
using Xunit.Abstractions;

namespace PresentationTests.EndPointsTests
{
    public class CommentEndpointsTests : IClassFixture<CommentTestFixture>
    {
        private readonly HttpClient _client;

        public CommentEndpointsTests(CommentTestFixture fixture)
        {
            _client = fixture.Client;
        }

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
