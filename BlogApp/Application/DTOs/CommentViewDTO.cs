namespace Application.DTOs;

/// <summary>
/// Represents a Data Transfer Object for a comment with user data.
/// </summary>
public class CommentViewDTO
{
    /// <summary>
    /// Gets or sets the body of the comment.
    /// </summary>
    /// <value>
    /// The content of the comment.
    /// </value>
    public required string Body { get; set; }

    /// <summary>
    /// Gets or sets the creation date of the comment.
    /// </summary>
    /// <value>
    /// The date and time when the comment was created.
    /// </value>
    public required DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the last update date of the comment.
    /// </summary>
    /// <value>
    /// The date and time when the comment was last updated.
    /// </value>
    public required DateTime UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets the user data associated with the comment.
    /// </summary>
    /// <value>
    /// An instance of <see cref="UserDataDTO"/> containing user information related to the comment.
    /// </value>
    public required UserDataDTO UserDataDTO { get; set; }
}
