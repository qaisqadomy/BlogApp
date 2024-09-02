namespace Application.DTOs;

/// <summary>
/// Represents a Data Transfer Object for viewing an article.
/// </summary>
public class ArticleViewDTO
{
    /// <summary>
    /// Gets or sets the slug of the article.
    /// </summary>
    /// <value>
    /// A unique identifier for the article, typically used in URLs.
    /// </value>
    public required string Slug { get; set; }

    /// <summary>
    /// Gets or sets the title of the article.
    /// </summary>
    /// <value>
    /// The title of the article, which should be descriptive.
    /// </value>
    public required string Title { get; set; }

    /// <summary>
    /// Gets or sets the description of the article.
    /// </summary>
    /// <value>
    /// A brief summary or description of the article.
    /// </value>
    public required string Description { get; set; }

    /// <summary>
    /// Gets or sets the body of the article.
    /// </summary>
    /// <value>
    /// The main content of the article.
    /// </value>
    public required string Body { get; set; }

    /// <summary>
    /// Gets or sets the tags associated with the article.
    /// </summary>
    /// <value>
    /// A list of tags that categorize or describe the article.
    /// </value>
    public List<string>? Tags { get; set; }

    /// <summary>
    /// Gets or sets the creation date of the article.
    /// </summary>
    /// <value>
    /// The date and time when the article was created.
    /// </value>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the last update date of the article.
    /// </summary>
    /// <value>
    /// The date and time when the article was last updated.
    /// </value>
    public DateTime UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the article is favorited.
    /// </summary>
    /// <value>
    /// <c>true</c> if the article is favorited; otherwise, <c>false</c>.
    /// </value>
    public bool Favorited { get; set; }

    /// <summary>
    /// Gets or sets the number of times the article has been favorited.
    /// </summary>
    /// <value>
    /// The count of favorites for the article.
    /// </value>
    public int FavoritesCount { get; set; }

    /// <summary>
    /// Gets or sets the user data associated with the article.
    /// </summary>
    /// <value>
    /// An instance of <see cref="UserDataDTO"/> representing the author or user associated with the article.
    /// </value>
    public required UserDataDTO UserDataDTO { get; set; }
}
