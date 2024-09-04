using Domain.Entities;
using Domain.Exeptions;
using Domain.IRepositories;
using Infrastructure.Data;

namespace Application.Repositories;

/// <summary>
/// Provides data access methods for managing articles in the database.
/// </summary>
public class ArticleRepository(AppDbContext context) : IArticleRepository
{
    private readonly AppDbContext _context = context;

    /// <summary>
    /// Adds a new article to the database.
    /// </summary>
    public void AddArticle(Article article)
    {
        _context.Articles.Add(article);
        _context.SaveChanges();
    }

    /// <summary>
    /// Retrieves all articles from the database.
    /// </summary>
    public List<Article> GetAll()
    {
        List<Article> list = _context.Articles.ToList();
        return list;
    }

    /// <summary>
    /// Retrieves articles from the database based on optional filters.
    /// </summary>
    public List<Article> GetArticle(string? tag, string? author, bool? favorited)
    {
        var query = _context.Articles.AsQueryable();

        if (!string.IsNullOrEmpty(author))
        {
            var user = _context.Users.FirstOrDefault(u => u.UserName == author) 
                ?? throw new ArticleNotFound($"Article with the user {author} not found");

            query = query.Where(a => a.AuthorId == user.Id);
        }

        if (!string.IsNullOrEmpty(tag))
        {
            query = query.Where(a => a.Tags!.Contains(tag));
        }

        if (favorited.HasValue)
        {
            query = query.Where(a => a.Favorited == favorited.Value);
        }

        var result = query.ToList();
        if (result.Count == 0)
        {
            throw new ArticleNotFound("Articles not found");
        }

        return result;
    }

    /// <summary>
    /// Updates an existing article in the database.
    /// </summary>
    public void UpdateArticle(Article article, int Id)
    {
        Article art = _context.Articles.FirstOrDefault(a => a.Id == Id)
            ?? throw new ArticleNotFound($"Article with the Id : {Id} not found");

        art.Title = article.Title;
        art.Description = article.Description;
        art.Body = article.Body;
        art.Tags = article.Tags;
        _context.Articles.Update(art);
        _context.SaveChanges();
    }

    /// <summary>
    /// Deletes an article from the database.
    /// </summary>
    public void DeleteArticle(int Id)
    {
        Article art = _context.Articles.FirstOrDefault(a => a.Id == Id)
            ?? throw new ArticleNotFound($"Article with the Id : {Id} not found");

        _context.Articles.Remove(art);
        _context.SaveChanges();
    }
}
