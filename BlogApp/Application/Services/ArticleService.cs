using Application.DTOs;
using Domain.Entities;
using Domain.IRepositories;

namespace Application.Services;

public class ArticleService(IArticleRepository articleRepository, IUserRepository userRepository)
{
    private readonly IArticleRepository articleRepository = articleRepository;
    private readonly IUserRepository userRepository = userRepository;

    public List<ArticleViewDTO> GetAll()
    {
        List<Article> articles = articleRepository.GetAll();

        var userIds = articles.Select(art => art.AuthorId).Distinct().ToList();
        var users = userRepository.GetByIds(userIds);

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
    public List<ArticleViewDTO> GetArticle(string? tag, string? author, bool? favorited)
    {
        List<Article> articles = articleRepository.GetArticle(tag, author, favorited);

        var userIds = articles.Select(art => art.AuthorId).Distinct().ToList();

        var users = userRepository.GetByIds(userIds);

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
    public void AddArticle(ArticleDTO art)
    {
        Article article1 = new()
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
        articleRepository.AddArticle(article1);
    }
    public void UpdateArticle(ArticleDTO art, int id)
    {
        Article article1 = new()
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
        articleRepository.UpdateArticle(article1, id);
    }
    public void DeleteArticle(int id)
    {
        articleRepository.DeleteArticle(id);
    }
}
