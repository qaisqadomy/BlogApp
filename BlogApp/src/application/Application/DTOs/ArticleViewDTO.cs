namespace Application.DTOs;

/// <summary>
/// Represents a Data Transfer Object for viewing an article.
/// </summary>
public class ArticleViewDTO
{
    /// <summary>
    /// Gets or sets the slug of the article.
    /// </summary>
  
    public required string Slug { get; set; }

    /// <summary>
    /// Gets or sets the title of the article.
    /// </summary>
   
    public required string Title { get; set; }

    /// <summary>
    /// Gets or sets the description of the article.
    /// </summary>
  
    public required string Description { get; set; }

    /// <summary>
    /// Gets or sets the body of the article.
    /// </summary>
 
    public required string Body { get; set; }

    /// <summary>
    /// Gets or sets the tags associated with the article.
    /// </summary>
 
    public List<string>? Tags { get; set; }

    /// <summary>
    /// Gets or sets the creation date of the article.
    /// </summary>
   
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the last update date of the article.
    /// </summary>
    
    public DateTime UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the article is favorited.
    /// </summary>
   
    public bool Favorited { get; set; }

    /// <summary>
    /// Gets or sets the number of times the article has been favorited.
    /// </summary>
       public int FavoritesCount { get; set; }

    /// <summary>
    /// Gets or sets the user data associated with the article.
    /// </summary>
    
    public required UserDataDTO UserDataDTO { get; set; }
}
