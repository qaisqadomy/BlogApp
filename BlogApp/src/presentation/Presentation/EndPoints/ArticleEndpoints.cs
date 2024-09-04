using Application.DTOs;
using Application.Services;

namespace Presentation.EndPoints;

/// <summary>
/// Contains the endpoint mappings for article-related operations.
/// </summary>
public static class ArticleEndpoints
{
    /// <summary>
    /// Maps the article-related endpoints to the specified <see cref="WebApplication"/> instance.
    /// </summary>
    public static void MapArticleEndpoint(this WebApplication app)
    {
        RouteGroupBuilder ArticleGroup = app.MapGroup("/article")
            .WithTags("articles");

        /// <summary>
        /// Gets a list of articles based on optional filters.
        /// </summary>
        ArticleGroup.MapGet("/", (string? tag, string? author, bool? favorited, ArticleService service) =>
        {
            List<ArticleViewDTO> articleDTO = service.GetArticle(tag, author, favorited);
            return Results.Ok(articleDTO);
        });

        /// <summary>
        /// Gets all articles.
        /// </summary>
        ArticleGroup.MapGet("/articles", (ArticleService service) =>
        {
            List<ArticleViewDTO> articleDTO = service.GetAll();
            return Results.Ok(articleDTO);
        });

        /// <summary>
        /// Adds a new article.
        /// </summary>
        ArticleGroup.MapPost("/", (ArticleDTO articleDTO, ArticleService service) =>
        {
            service.AddArticle(articleDTO);
            return Results.Ok(articleDTO);
        });

        /// <summary>
        /// Updates an existing article.
        /// </summary>
        ArticleGroup.MapPut("/{id}", (int id, ArticleDTO articleDTO, ArticleService service) =>
        {
            service.UpdateArticle(articleDTO, id);
            return Results.Ok(articleDTO);
        });

        /// <summary>
        /// Deletes an article.
        /// </summary>
        ArticleGroup.MapDelete("/{id}", (int id, ArticleService service) =>
        {
            service.DeleteArticle(id);
            return Results.NoContent();
        });
    }
}
