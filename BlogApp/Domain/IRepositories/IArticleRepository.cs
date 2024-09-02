using Domain.Entities;

namespace Domain.IRepositories
{
    /// <summary>
    /// Defines the interface for the repository responsible for managing articles.
    /// </summary>
    public interface IArticleRepository
    {
        /// <summary>
        /// Retrieves all articles from the repository.
        /// </summary>
        /// <returns>A list of all articles.</returns>
        List<Article> GetAll();

        /// <summary>
        /// Retrieves a list of articles based on the specified filters.
        /// </summary>
        /// <param name="tag">The tag to filter articles by.</param>
        /// <param name="author">The author to filter articles by.</param>
        /// <param name="favorited">A flag indicating whether to filter by favorited articles.</param>
        /// <returns>A list of articles that match the specified filters.</returns>
        List<Article> GetArticle(string? tag, string? author, bool? favorited);

        /// <summary>
        /// Adds a new article to the repository.
        /// </summary>
        /// <param name="article">The article to add.</param>
        void AddArticle(Article article);

        /// <summary>
        /// Updates an existing article in the repository.
        /// </summary>
        /// <param name="article">The article with updated information.</param>
        /// <param name="id">The identifier of the article to update.</param>
        void UpdateArticle(Article article, int id);

        /// <summary>
        /// Deletes an article from the repository.
        /// </summary>
        /// <param name="id">The identifier of the article to delete.</param>
        void DeleteArticle(int id);
    }
}
