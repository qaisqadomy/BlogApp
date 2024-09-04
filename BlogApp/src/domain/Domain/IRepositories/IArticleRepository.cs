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
   
        List<Article> GetAll();

        /// <summary>
        /// Retrieves a list of articles based on the specified filters.
        /// </summary>
     
        List<Article> GetArticle(string? tag, string? author, bool? favorited);

        /// <summary>
        /// Adds a new article to the repository.
        /// </summary>
   
        void AddArticle(Article article);

        /// <summary>
        /// Updates an existing article in the repository.
        /// </summary>
     
        void UpdateArticle(Article article, int id);

        /// <summary>
        /// Deletes an article from the repository.
        /// </summary>
 
        void DeleteArticle(int id);
    }
}
