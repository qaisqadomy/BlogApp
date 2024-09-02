namespace Application.DTOs;

/// <summary>
/// Represents a Data Transfer Object for user information required for user registration or login.
/// </summary>
public class UserDTO
{
    /// <summary>
    /// Gets or sets the username of the user.
    /// </summary>
    
    public required string UserName { get; set; }

    /// <summary>
    /// Gets or sets the email address of the user.
    /// </summary>
 
    public required string Email { get; set; }

    /// <summary>
    /// Gets or sets the password for the user.
    /// </summary>
  
    public required string Password { get; set; }
}
