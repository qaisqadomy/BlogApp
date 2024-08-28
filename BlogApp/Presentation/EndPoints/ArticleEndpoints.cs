using Application.DTOs;
using Application.Services;

namespace Presentation.EndPoints;

public static class ArticleEndpoints
{

    public static void MapArticleEndpoint(this WebApplication app)
    {
        RouteGroupBuilder ArticleGroup = app.MapGroup("/article")
             .WithTags("articles");

        ArticleGroup.MapGet("/", (string? tag, string? author, bool? favorited, ArticleService service) =>
    {
        List<ArticleDTO> articleDTO = service.GetArticle(tag, author, favorited);
        return Results.Ok(articleDTO);
    });

        ArticleGroup.MapGet("/articles", (ArticleService service) =>
        {
            List<ArticleDTO> articleDTO = service.GetAll();
            return Results.Ok(articleDTO);
        });
        ArticleGroup.MapPost("/", (ArticleDTO articleDTO, ArticleService service) =>
       {
           service.AddArticle(articleDTO);
           return Results.Ok(articleDTO);
       });

        ArticleGroup.MapPut("/{id}", (int id, ArticleDTO articleDTO, ArticleService service) =>
   {
       service.UpdateArticle(articleDTO, id);
       return Results.Ok(articleDTO);
   });
ArticleGroup.MapDelete("/{id}", (int id, ArticleService service) =>
{
    service.DeleteArticle(id);
    return Results.NoContent();
});
    }
}

