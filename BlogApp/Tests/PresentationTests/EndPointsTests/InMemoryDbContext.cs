using System;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace PresentationTests.EndPointsTests;

public class InMemoryDbContext
{
    public AppDbContext DbContext { get; private set; }

    public InMemoryDbContext()
    {
        string uniqueId = Guid.NewGuid().ToString();
        DbContextOptions<AppDbContext> options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase($"TestDatabase_{uniqueId}")
            .Options;

        DbContext = new AppDbContext(options);
    }

}
