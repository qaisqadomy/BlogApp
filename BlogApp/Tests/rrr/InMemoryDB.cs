using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace RepositoriesTests;

public class InMemoryDB
{
    public AppDbContext DbContext { get; private set; }

    public InMemoryDB()
    {
        string uniqueId = Guid.NewGuid().ToString();
        DbContextOptions<AppDbContext> options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase($"TestDatabase_{uniqueId}")
            .Options;

        DbContext = new AppDbContext(options);
    }
}

