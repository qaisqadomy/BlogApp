using Domain.Entities;
using Domain.Exeptions;
using Domain.IRepositories;
using Infrastructure.Data;

namespace Application.Repositories;

/// <summary>
/// Provides data access methods for managing articles in the database.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="ArticleRepository"/> class.
/// </remarks>
/// <param name="context">The <see cref="AppDbContext"/> used for database operations.</param>
public class ArticleRepository(AppDbContext context) : IArticleRepository
{
    private readonly AppDbContext _context = context;

    /// <summary>
    /// Adds a new article to the database.
    /// </summary>
    /// <param name="article">The article to add.</param>
    public void AddArticle(Article article)
    {
        _context.Articles.Add(article);
        _context.SaveChanges();
    }

    /// <summary>
    /// Retrieves all articles from the database.
    /// </summary>
    /// <returns>A list of all articles.</returns>
    public List<Article> GetAll()
    {
        List<Article> list = _context.Articles.ToList();
        return list;
    }

    /// <summary>
    /// Retrieves articles from the database based on optional filters.
    /// </summary>
    /// <param name="tag">The tag to filter articles by.</param>
    /// <param name="author">The author to filter articles by.</param>
    /// <param name="favorited">Indicates whether to filter by favorited status.</param>
    /// <returns>A list of articles matching the specified filters.</returns>
    /// <exception cref="ArticleNotFound">Thrown when no articles are found that match the specified filters.</exception>
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
    /// <param name="article">The updated article information.</param>
    /// <param name="Id">The ID of the article to update.</param>
    /// <exception cref="ArticleNotFound">Thrown when the article with the specified ID is not found.</exception>
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
    /// <param name="Id">The ID of the article to delete.</param>
    /// <exception cref="ArticleNotFound">Thrown when the article with the specified ID is not found.</exception>
    public void DeleteArticle(int Id)
    {
        Article art = _context.Articles.FirstOrDefault(a => a.Id == Id)
            ?? throw new ArticleNotFound($"Article with the Id : {Id} not found");

        _context.Articles.Remove(art);
        _context.SaveChanges();
    }
}
