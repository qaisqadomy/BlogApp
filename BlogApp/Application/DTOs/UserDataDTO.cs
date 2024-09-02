namespace Application.DTOs;

/// <summary>
/// Represents a Data Transfer Object for user data including profile details.
/// </summary>
public class UserDataDTO
{
    /// <summary>
    /// Gets or sets the unique identifier of the user.
    /// </summary>
    /// <value>
    /// The unique identifier of the user.
    /// </value>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the username of the user.
    /// </summary>
    /// <value>
    /// The username of the user.
    /// </value>
    public required string UserName { get; set; }

    /// <summary>
    /// Gets or sets the email address of the user.
    /// </summary>
    /// <value>
    /// The email address of the user.
    /// </value>
    public required string Email { get; set; }

    /// <summary>
    /// Gets or sets the bio or description of the user.
    /// </summary>
    /// <value>
    /// The bio or description of the user.
    /// </value>
    public string? Bio { get; set; }

    /// <summary>
    /// Gets or sets the URL of the user's profile image.
    /// </summary>
    /// <value>
    /// The URL of the user's profile image.
    /// </value>
    public string? Image { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the user is being followed.
    /// </summary>
    /// <value>
    /// <c>true</c> if the user is being followed; otherwise, <c>false</c>.
    /// </value>
    public bool Following { get; set; }
}
