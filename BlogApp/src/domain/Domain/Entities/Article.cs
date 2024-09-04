using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

/// <summary>
/// Represents an article with related metadata and content.
/// </summary>
public class Article
{
    /// <summary>
    /// Gets or sets the unique identifier for the article.
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the slug for the article. This should be unique and used in the URL.
    /// </summary>
    [Required]
    public string Slug { get; set; } = null!;

    /// <summary>
    /// Gets or sets the title of the article.
    /// </summary>
    [Required]
    public string Title { get; set; } = null!;

    /// <summary>
    /// Gets or sets the description of the article, which provides a brief summary.
    /// </summary>
    [Required]
    public string Description { get; set; } = null!;

    /// <summary>
    /// Gets or sets the body of the article, which contains the main content.
    /// </summary>
    [Required]
    public string Body { get; set; } = null!;

    /// <summary>
    /// Gets or sets the tags associated with the article. This is used to categorize or tag the article with keywords.
    /// </summary>
    public List<string>? Tags { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the article was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the article was last updated.
    /// </summary>
    public DateTime UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the article is favorited by users.
    /// </summary>
    public bool Favorited { get; set; }

    /// <summary>
    /// Gets or sets the number of times the article has been favorited by users.
    /// </summary>
    public int FavoritesCount { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the author of the article.
    /// </summary>
    [Required]
    public int AuthorId { get; set; }
}
