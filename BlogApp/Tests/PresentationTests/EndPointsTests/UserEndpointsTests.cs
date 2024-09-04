using System.Text;
using System.Text.Json;
using Application.DTOs;
using System.Net;
using System.Net.Http.Headers;
using Presentation;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc.Testing;

namespace PresentationTests.EndPointsTests;

public class UserEndpointsTests(UserTestFixture fixture) : IClassFixture<UserTestFixture>
{
    private readonly HttpClient _client = fixture.Client;
    private readonly UserTestFixture fixture = fixture;

    [Fact]
    public async Task RegisterUser_ShouldReturnOk()
    {

        var userDto = TestHelper.UserDto();

        var content = new StringContent(JsonSerializer.Serialize(userDto), Encoding.UTF8, "application/json");


        var response = await _client.PostAsync("/user/register", content);


        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task LoginUser_ShouldReturnToken()
    {
         string email = TestHelper.User1().Email;
        string password = TestHelper.User1().Password;

        var response = await _client.PostAsync($"/user/login?Email={email}&Password={password}", null);

        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();
        Assert.False(string.IsNullOrEmpty(responseString));
    }
    /* [Fact]
     public async Task GetUser_ShouldReturnUnauthorized_WhenNoToken()
     {
         var response = await _client.GetAsync("/user/");
         response.EnsureSuccessStatusCode();

         Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
     }
 */
    [Fact]
    public async Task GetUser_ShouldReturnOkWithUser_WhenAuthorized()
    {

        string email = TestHelper.User1().Email;
        string password = TestHelper.User1().Password;

        var loginResponse = await _client.PostAsync($"/user/login?Email={email}&Password={password}", null);
        loginResponse.EnsureSuccessStatusCode();
        var responseString = await loginResponse.Content.ReadAsStringAsync();
        var token = JsonSerializer.Deserialize<string>(responseString); // Adjust if necessary

        // Use the valid token to authorize the request
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var userResponse = await _client.GetAsync("/user/");
        userResponse.EnsureSuccessStatusCode();
        responseString = await userResponse.Content.ReadAsStringAsync();
        Console.WriteLine(responseString); // Log response

        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var user = JsonSerializer.Deserialize<GetUserDTO>(responseString, options);

        Assert.NotNull(user);
        Assert.Equal(TestHelper.User1().UserName, user.UserName);
    }

    [Fact]
    public async Task UpdateUser_ShouldReturnOk_WhenAuthorized()
    {
        string email = TestHelper.User1().Email;
        string password = TestHelper.User1().Password;

        var loginResponse = await _client.PostAsync($"/user/login?Email={email}&Password={password}", null);
        loginResponse.EnsureSuccessStatusCode();
        var responseString = await loginResponse.Content.ReadAsStringAsync();
        var token = JsonSerializer.Deserialize<string>(responseString);

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var userDto = TestHelper.UserDto();

        var content = new StringContent(JsonSerializer.Serialize(userDto), Encoding.UTF8, "application/json");
        var response = await _client.PutAsync("/user/", content);

        response.EnsureSuccessStatusCode();
        responseString = await response.Content.ReadAsStringAsync();
        var updatedUser = JsonSerializer.Deserialize<UserDTO>(responseString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        Assert.NotNull(updatedUser);
        Assert.Equal(userDto.UserName, updatedUser.UserName);
        Assert.Equal(userDto.Email, updatedUser.Email);
    }

}
public class UserTestFixture : IDisposable
{
    public HttpClient Client { get; private set; }
    private readonly WebApplicationFactory<Program> _factory;

    public UserTestFixture()
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
                        options.UseInMemoryDatabase("InMemoryDbForTesting11");
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

        context.Users.RemoveRange(context.Users);
        context.SaveChanges();


        var users = new List<User>
            {
                TestHelper.User1(),
                TestHelper.User2()
            };

        context.Users.AddRange(users);
        context.SaveChanges();
    }


    public void Dispose()
    {
        _factory.Dispose();
        Client?.Dispose();
    }
}


