namespace Application.DTOs;

/// <summary>
/// Represents a Data Transfer Object for user data including profile details.
/// </summary>
public class UserDataDTO
{
    /// <summary>
    /// Gets or sets the unique identifier of the user.
    /// </summary>
   
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the username of the user.
    /// </summary>
    
    public required string UserName { get; set; }

    /// <summary>
    /// Gets or sets the email address of the user.
    /// </summary>
    
    public required string Email { get; set; }

    /// <summary>
    /// Gets or sets the bio or description of the user.
    /// </summary>
 
    public string? Bio { get; set; }

    /// <summary>
    /// Gets or sets the URL of the user's profile image.
    /// </summary>
  
    public string? Image { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the user is being followed.
    /// </summary>
   
    public bool Following { get; set; }
}
