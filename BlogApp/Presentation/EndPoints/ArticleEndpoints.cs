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
    /// <param name="app">The <see cref="WebApplication"/> instance to map the endpoints on.</param>
    public static void MapArticleEndpoint(this WebApplication app)
    {
        RouteGroupBuilder ArticleGroup = app.MapGroup("/article")
            .WithTags("articles");

        /// <summary>
        /// Gets a list of articles based on optional filters.
        /// </summary>
        /// <param name="tag">Optional filter by tag.</param>
        /// <param name="author">Optional filter by author.</param>
        /// <param name="favorited">Optional filter by favorited status.</param>
        /// <param name="service">The <see cref="ArticleService"/> instance used to get the articles.</param>
        /// <returns>A list of <see cref="ArticleViewDTO"/> representing the filtered articles.</returns>
        ArticleGroup.MapGet("/", (string? tag, string? author, bool? favorited, ArticleService service) =>
        {
            List<ArticleViewDTO> articleDTO = service.GetArticle(tag, author, favorited);
            return Results.Ok(articleDTO);
        });

        /// <summary>
        /// Gets all articles.
        /// </summary>
        /// <param name="service">The <see cref="ArticleService"/> instance used to get the articles.</param>
        /// <returns>A list of <see cref="ArticleViewDTO"/> representing all articles.</returns>
        ArticleGroup.MapGet("/articles", (ArticleService service) =>
        {
            List<ArticleViewDTO> articleDTO = service.GetAll();
            return Results.Ok(articleDTO);
        });

        /// <summary>
        /// Adds a new article.
        /// </summary>
        /// <param name="articleDTO">The <see cref="ArticleDTO"/> representing the article to add.</param>
        /// <param name="service">The <see cref="ArticleService"/> instance used to add the article.</param>
        /// <returns>The added <see cref="ArticleDTO"/>.</returns>
        ArticleGroup.MapPost("/", (ArticleDTO articleDTO, ArticleService service) =>
        {
            service.AddArticle(articleDTO);
            return Results.Ok(articleDTO);
        });

        /// <summary>
        /// Updates an existing article.
        /// </summary>
        /// <param name="id">The ID of the article to update.</param>
        /// <param name="articleDTO">The <see cref="ArticleDTO"/> representing the updated article data.</param>
        /// <param name="service">The <see cref="ArticleService"/> instance used to update the article.</param>
        /// <returns>The updated <see cref="ArticleDTO"/>.</returns>
        ArticleGroup.MapPut("/{id}", (int id, ArticleDTO articleDTO, ArticleService service) =>
        {
            service.UpdateArticle(articleDTO, id);
            return Results.Ok(articleDTO);
        });

        /// <summary>
        /// Deletes an article.
        /// </summary>
        /// <param name="id">The ID of the article to delete.</param>
        /// <param name="service">The <see cref="ArticleService"/> instance used to delete the article.</param>
        /// <returns>An HTTP response with no content if the deletion was successful.</returns>
        ArticleGroup.MapDelete("/{id}", (int id, ArticleService service) =>
        {
            service.DeleteArticle(id);
            return Results.NoContent();
        });
    }
}
