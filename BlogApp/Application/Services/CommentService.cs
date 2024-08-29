using Application.DTOs;
using Domain.Entities;
using Domain.IRepositories;

namespace Application.Services;

public class CommentService
{
    private readonly ICommentRepository commentRepository;
    private readonly IUserRepository userRepository;

    public CommentService(ICommentRepository commentRepository,IUserRepository userRepository)
    {
        this.commentRepository = commentRepository;
        this.userRepository =userRepository;
    }
   public List<CommentViewDTO> GetAll()
{
    List<Comment> comments = commentRepository.GetAll();

    var userIds = comments.Select(c => c.AuthorId).Distinct().ToList();

    var users = userRepository.GetByIds(userIds);

    return comments.Select(comment =>
    {
        var user = users.FirstOrDefault(u => u.Id == comment.AuthorId);

        return new CommentViewDTO
        {
            Body = comment.Body,
            CreatedAt = comment.CreatedAt,
            UpdatedAt = comment.UpdatedAt,
            UserDataDTO = new UserDataDTO
            {
                Id = comment.AuthorId,
                UserName = user?.UserName ?? "Unknown",
                Email = user?.Email ?? "Unknown",
                Bio = user?.Bio ?? "Unknown",
                Image = user?.Image ?? "Unknown",
                Following = user?.Following ?? false
            }
        };
    }).ToList();
}

    public void AddComment(CommentDTO comment)
    {
        Comment comment1 = new Comment
        {
            Body = comment.Body,
            CreatedAt = comment.CreatedAt,
            UpdatedAt = comment.UpdatedAt,
            AuthorId = comment.AuthorId
        };
        commentRepository.AddComment(comment1);
    }
    public void DeleteComment(int id)
    {
        commentRepository.DeleteComment(id);
    }
}
