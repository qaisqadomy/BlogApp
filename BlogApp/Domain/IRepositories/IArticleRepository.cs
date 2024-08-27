using System;
using Domain.Aggregates;

namespace Domain.IRepositories;

public interface IArticleRepository
{
    public List<Article> GetAll();
    public List<Article> GetArticle(string tag, string author, bool Favorited);
    public void AddArticle (Article article);
    public void UpdateArticle (Article article);
    public void DeleteArticle (Article article);
}
