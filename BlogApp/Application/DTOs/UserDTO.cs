namespace Application.DTOs;

/// <summary>
/// Represents a Data Transfer Object for user information required for user registration or login.
/// </summary>
public class UserDTO
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

    /// <summary>
    /// Gets or sets the password for the user.
    /// </summary>
    /// <value>
    /// The password of the user.
    /// </value>
    public required string Password { get; set; }
}
