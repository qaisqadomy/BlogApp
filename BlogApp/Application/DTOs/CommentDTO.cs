namespace Application.DTOs;

public class CommentDTO
{
 public required string Body { get; set; }
    public required DateTime CreatedAt { get; set; }
    public required DateTime UpdatedAt { get; set; }
    public required UserDataDTO Author { get; set; }
}
