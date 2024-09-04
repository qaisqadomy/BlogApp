namespace Application.DTOs;

/// <summary>
/// Represents a Data Transfer Object for a comment.
/// </summary>
public class CommentDTO
{
    /// <summary>
    /// Gets or sets the body of the comment.
    /// </summary>
   
    public required string Body { get; set; }

    /// <summary>
    /// Gets or sets the creation date of the comment.
    /// </summary>
   
    public required DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the last update date of the comment.
    /// </summary>
   
    public required DateTime UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets the ID of the author of the comment.
    /// </summary>
   
    public required int AuthorId { get; set; }
}
