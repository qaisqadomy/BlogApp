using System;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Presentation;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace PresentationTests.EndPointsTests
{
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
            // Dispose of the factory to release resources
            _factory.Dispose();

            // Dispose of the HttpClient
            Client?.Dispose();
        }
    }
}
