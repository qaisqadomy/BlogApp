namespace Application.DTOs;

/// <summary>
/// Represents a Data Transfer Object for retrieving user information.
/// </summary>
public class GetUserDTO
{
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
}
