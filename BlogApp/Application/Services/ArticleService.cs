using Application.DTOs;
using Domain.Entities;
using Domain.IRepositories;

namespace Application.Services;

public class ArticleService
{
    private readonly IArticleRepository articleRepository;
    public ArticleService(IArticleRepository articleRepository)
    {
        this.articleRepository = articleRepository;
        
    }
    public List<ArticleDTO> GetAll(){
        List<Article> articles =articleRepository.GetAll();

       return articles.Select(art => new ArticleDTO
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
        AuthorId =  art.AuthorId
    }).ToList();

    }
    public List<ArticleDTO> GetArticle(string? tag, string? author, bool? Favorited){
        List<Article> articles = articleRepository.GetArticle(tag,author,Favorited);
         return articles.Select(art => new ArticleDTO
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
            AuthorId =  art.AuthorId
    }).ToList();
    }
    public void AddArticle(ArticleDTO art){
        Article article1 = new Article{
            Slug = art.Slug,
        Title = art.Title,
        Description = art.Description,
        Body = art.Body,
        Tags = art.Tags,
        CreatedAt = art.CreatedAt,
        UpdatedAt = art.UpdatedAt,
        Favorited = art.Favorited,
        FavoritesCount = art.FavoritesCount,
               AuthorId =  art.AuthorId

        } ;
        articleRepository.AddArticle(article1);
    }
    public void UpdateArticle(ArticleDTO art,int id){
          Article article1 = new Article{
            Slug = art.Slug,
        Title = art.Title,
        Description = art.Description,
        Body = art.Body,
        Tags = art.Tags,
        CreatedAt = art.CreatedAt,
        UpdatedAt = art.UpdatedAt,
        Favorited = art.Favorited,
        FavoritesCount = art.FavoritesCount,
               AuthorId =  art.AuthorId
        } ;
        articleRepository.UpdateArticle(article1,id);
    }
    public void DeleteArticle(int id){
        
        articleRepository.DeleteArticle(id);
    }
}
