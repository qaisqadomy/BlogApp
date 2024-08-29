namespace Application.DTOs;

public class CommentViewDTO
{
    public required string Body { get; set; }
    public required DateTime CreatedAt { get; set; }
    public required DateTime UpdatedAt { get; set; }
    public required UserDataDTO UserDataDTO { get; set; }
}
