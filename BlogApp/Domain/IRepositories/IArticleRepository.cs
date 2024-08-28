using Domain.Entities;

namespace Domain.IRepositories;

public interface IArticleRepository
{
    public List<Article> GetAll();
    public List<Article> GetArticle(string? tag, string? author, bool? Favorited);
    public void AddArticle (Article article);
    public void UpdateArticle (Article article,int Id);
    public void DeleteArticle ( int Id);
}
