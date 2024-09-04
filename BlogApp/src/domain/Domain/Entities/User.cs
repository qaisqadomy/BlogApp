using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

/// <summary>
/// Represents a user in the system.
/// </summary>
public class User
{
    /// <summary>
    /// Gets or sets the unique identifier for the user.
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the username of the user.
    /// </summary>
    [Required]
    public string UserName { get; set; } = null!;

    /// <summary>
    /// Gets or sets the email address of the user.
    /// </summary>
    [Required]
    public string Email { get; set; } = null!;

    /// <summary>
    /// Gets or sets the password for the user's account.
    /// </summary>
    [Required]
    public string Password { get; set; } = null!;

    /// <summary>
    /// Gets or sets a short biography or description about the user.
    /// This property is optional.
    /// </summary>
    public string? Bio { get; set; }

    /// <summary>
    /// Gets or sets the URL to the user's profile image.
    /// This property is optional.
    /// </summary>
    public string? Image { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the user is following another user.
    /// </summary>
    public bool Following { get; set; }
}
