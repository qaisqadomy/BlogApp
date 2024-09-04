using Application.DTOs;
using Domain.Entities;
using Domain.IRepositories;

namespace Application.Services;

/// <summary>
/// Provides services for managing articles, including retrieval, creation, update, and deletion.
/// </summary>
public class ArticleService(IArticleRepository articleRepository, IUserRepository userRepository)
{
    private readonly IArticleRepository _articleRepository = articleRepository;
    private readonly IUserRepository _userRepository = userRepository;

    /// <summary>
    /// Retrieves all articles and their associated user data.
    /// </summary>
    public List<ArticleViewDTO> GetAll()
    {
        List<Article> articles = _articleRepository.GetAll();

        var userIds = articles.Select(art => art.AuthorId).Distinct().ToList();
        var users = _userRepository.GetByIds(userIds);

        return articles.Select(art =>
        {
            var user = users.FirstOrDefault(u => u.Id == art.AuthorId);

            return new ArticleViewDTO
            {
                Slug = art.Slug,
                Title = art.Title,
                Description = art.Description,
                Body = art.Body,
                Tags = art.Tags,
                CreatedAt = art.CreatedAt,
                UpdatedAt = art.UpdatedAt,
                Favorited = art.Favorited,
                FavoritesCount = art.FavoritesCount,
                UserDataDTO = new UserDataDTO
                {
                    Id = art.AuthorId,
                    UserName = user?.UserName ?? "Unknown",
                    Email = user?.Email ?? "Unknown",
                    Bio = user?.Bio ?? "Unknown",
                    Image = user?.Image ?? "Unknown",
                    Following = user?.Following ?? false
                }
            };
        }).ToList();
    }

    /// <summary>
    /// Retrieves articles based on specified filters and their associated user data.
    /// </summary>
    public List<ArticleViewDTO> GetArticle(string? tag, string? author, bool? favorited)
    {
        List<Article> articles = _articleRepository.GetArticle(tag, author, favorited);

        var userIds = articles.Select(art => art.AuthorId).Distinct().ToList();
        var users = _userRepository.GetByIds(userIds);

        return articles.Select(art =>
        {
            var user = users.FirstOrDefault(u => u.Id == art.AuthorId);

            return new ArticleViewDTO
            {
                Slug = art.Slug,
                Title = art.Title,
                Description = art.Description,
                Body = art.Body,
                Tags = art.Tags,
                CreatedAt = art.CreatedAt,
                UpdatedAt = art.UpdatedAt,
                Favorited = art.Favorited,
                FavoritesCount = art.FavoritesCount,
                UserDataDTO = new UserDataDTO
                {
                    Id = art.AuthorId,
                    UserName = user?.UserName ?? "Unknown",
                    Email = user?.Email ?? "Unknown",
                    Bio = user?.Bio ?? "Unknown",
                    Image = user?.Image ?? "Unknown",
                    Following = user?.Following ?? false
                }
            };
        }).ToList();
    }

    /// <summary>
    /// Adds a new article to the repository.
    /// </summary>
    public void AddArticle(ArticleDTO art)
    {
        Article article = new()
        {
            Slug = art.Slug,
            Title = art.Title,
            Description = art.Description,
            Body = art.Body,
            Tags = art.Tags,
            CreatedAt = art.CreatedAt,
            UpdatedAt = art.UpdatedAt,
            Favorited = art.Favorited,
            FavoritesCount = art.FavoritesCount,
            AuthorId = art.AuthorId
        };

        _articleRepository.AddArticle(article);
    }

    /// <summary>
    /// Updates an existing article in the repository.
    /// </summary>
    public void UpdateArticle(ArticleDTO art, int id)
    {
        Article article = new()
        {
            Slug = art.Slug,
            Title = art.Title,
            Description = art.Description,
            Body = art.Body,
            Tags = art.Tags,
            CreatedAt = art.CreatedAt,
            UpdatedAt = art.UpdatedAt,
            Favorited = art.Favorited,
            FavoritesCount = art.FavoritesCount,
            AuthorId = art.AuthorId
        };

        _articleRepository.UpdateArticle(article, id);
    }

    /// <summary>
    /// Deletes an article from the repository.
    /// </summary>
    public void DeleteArticle(int id)
    {
        _articleRepository.DeleteArticle(id);
    }
}
