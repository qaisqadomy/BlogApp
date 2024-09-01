using System;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Presentation;
using Domain.Entities;

namespace PresentationTests.EndPointsTests;

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
                        options.UseInMemoryDatabase($"InMemoryDbForTesting");
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

        context.Articles.AddRange(
            new Article
            {
                Slug = "QASS",
                Description = "DSDS",
                Title = "Sample Article 1",
                Body = "Content for sample article 1.",
                AuthorId = 1,
                Tags = new List<string> { "Tag1" },
                Favorited = false,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Article
            {
                Slug = "QASS",
                Description = "DSDS",
                Title = "Sample Article 2",
                Body = "Content for sample article 2.",
                AuthorId = 2,
                Tags = new List<string> { "Tag2" },
                Favorited = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }
        );
        context.SaveChanges();
    }

    public void Dispose()
    {
        // Dispose of the factory to release resources
        _factory.Dispose();

        // Dispose of the HttpClient
        Client?.Dispose();
    }
}