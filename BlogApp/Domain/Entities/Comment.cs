using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

/// <summary>
/// Represents a comment associated with an article.
/// </summary>
public class Comment
{
    /// <summary>
    /// Gets or sets the unique identifier for the comment.
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the body of the comment, which contains the main content.
    /// </summary>
    [Required]
    public string Body { get; set; } = null!;

    /// <summary>
    /// Gets or sets the date and time when the comment was created.
    /// </summary>
    [Required]
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the comment was last updated.
    /// </summary>
    [Required]
    public DateTime UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the author of the comment.
    /// </summary>
    [Required]
    public int AuthorId { get; set; }
}
