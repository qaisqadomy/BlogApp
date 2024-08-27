using System.ComponentModel.DataAnnotations;
using Domain.Entities;

namespace Domain.Aggregates;

public class Comment
{
    [Key]
    public int Id { get; set; }
    public required string Body { get; set; }
    public required DateTime CreatedAt { get; set; }
    public required DateTime UpdatedAt { get; set; }
    public required User Author { get; set; }

}
