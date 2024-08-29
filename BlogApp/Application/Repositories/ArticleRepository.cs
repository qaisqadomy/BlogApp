using Domain.Entities;
using Domain.Exeptions;
using Domain.IRepositories;
using Infrastructure.Data;

namespace Application.Repositories;

public class ArticleRepository : IArticleRepository
{
    private readonly AppDbContext context;
    public ArticleRepository(AppDbContext context)
    {
        this.context = context;
    }
    public void AddArticle(Article article)
    {
        context.Articles.Add(article);
        context.SaveChanges();
    }
    public List<Article> GetAll()
    {
        List<Article> list = [.. context.Articles];
        return list;
    }
    public List<Article> GetArticle(string? tag, string? author, bool? favorited)
    {
        var query = context.Articles.AsQueryable();

        if (!string.IsNullOrEmpty(author))
        {
            var user = context.Users.FirstOrDefault(u => u.UserName == author);

            if (user != null)
            {
                query = query.Where(a => a.AuthorId == user.Id);
            }
            else
            {
                return [];
            }
        }
        if (!string.IsNullOrEmpty(tag))
        {
            query = query.Where(a => a.Tags!.Contains(tag));
        }
        if (favorited.HasValue)
        {
            query = query.Where(a => a.Favorited == favorited.Value);
        }
        return [.. query];
    }

    public void UpdateArticle(Article article, int Id)
    {
        Article art = context.Articles.FirstOrDefault(a => a.Id == Id)!;
        if (art != null)
        {
            art.Title = article.Title;
            art.Description = article.Description;
            art.Body = article.Body;
            art.Tags = article.Tags;
            context.Articles.Update(art);
            context.SaveChanges();
        }
        else { throw new ArticleNotFound($"article with the Id : {Id} not found"); }
    }
    public void DeleteArticle(int Id)
    {
        Article art = context.Articles.FirstOrDefault(a => a.Id == Id)!;
        if (art == null)
        {
            throw new ArticleNotFound($"article with the Id : {Id} not found");
        }
        else
        {
            context.Articles.Remove(art);
            context.SaveChanges();
        }
    }
}
