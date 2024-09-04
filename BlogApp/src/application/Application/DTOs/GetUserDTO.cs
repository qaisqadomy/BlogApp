namespace Application.DTOs;

/// <summary>
/// Represents a Data Transfer Object for retrieving user information.
/// </summary>
public class GetUserDTO
{
    /// <summary>
    /// Gets or sets the username of the user.
    /// </summary>
  
    public required string UserName { get; set; }

    /// <summary>
    /// Gets or sets the email address of the user.
    /// </summary>
 
    public required string Email { get; set; }
}
