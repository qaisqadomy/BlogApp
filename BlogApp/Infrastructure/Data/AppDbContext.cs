using Domain.Aggregates;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }
    public DbSet<User> users { get; set; }
    public DbSet<Article> articles { get; set; }
    public DbSet<Tag>  tags { get; set; }
    public DbSet<Comment> comments { get; set; }
}
