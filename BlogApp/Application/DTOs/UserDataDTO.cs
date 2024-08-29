namespace Application.DTOs;

public class UserDataDTO
{
    public int Id { get; set; }
    public required string UserName { get; set; }
    public required string Email { get; set; }
    public string? Bio { get; set; }
    public string? Image { get; set; }
    public bool Following { get; set; }
}
