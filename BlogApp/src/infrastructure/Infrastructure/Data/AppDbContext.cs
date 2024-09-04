using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

/// <summary>
/// Represents the application's database context for Entity Framework Core.
/// </summary>
public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    /// <summary>
    /// Gets or sets the <see cref="DbSet{User}"/> for users in the database.
    /// </summary>
    public DbSet<User> Users { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="DbSet{Article}"/> for articles in the database.
    /// </summary>
    public DbSet<Article> Articles { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="DbSet{Comment}"/> for comments in the database.
    /// </summary>
    public DbSet<Comment> Comments { get; set; }

    // Override the OnModelCreating method to configure the model
    // using the Fluent API if needed.
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
