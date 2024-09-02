using Application.DTOs;
using Domain.Entities;
using Domain.IRepositories;

namespace Application.Services;

/// <summary>
/// Provides services for managing comments, including retrieval, creation, and deletion.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="CommentService"/> class.
/// </remarks>
/// <param name="commentRepository">The <see cref="ICommentRepository"/> used for comment data operations.</param>
/// <param name="userRepository">The <see cref="IUserRepository"/> used for user data operations.</param>
public class CommentService(ICommentRepository commentRepository, IUserRepository userRepository)
{
    private readonly ICommentRepository _commentRepository = commentRepository;
    private readonly IUserRepository _userRepository = userRepository;

    /// <summary>
    /// Retrieves all comments and their associated user data.
    /// </summary>
    /// <returns>A list of <see cref="CommentViewDTO"/> representing all comments.</returns>
    public List<CommentViewDTO> GetAll()
    {
        List<Comment> comments = _commentRepository.GetAll();

        var userIds = comments.Select(c => c.AuthorId).Distinct().ToList();
        var users = _userRepository.GetByIds(userIds);

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

    /// <summary>
    /// Adds a new comment to the repository.
    /// </summary>
    /// <param name="comment">The <see cref="CommentDTO"/> representing the comment to add.</param>
    public void AddComment(CommentDTO comment)
    {
        Comment commentEntity = new()
        {
            Body = comment.Body,
            CreatedAt = comment.CreatedAt,
            UpdatedAt = comment.UpdatedAt,
            AuthorId = comment.AuthorId
        };

        _commentRepository.AddComment(commentEntity);
    }

    /// <summary>
    /// Deletes a comment from the repository.
    /// </summary>
    /// <param name="id">The ID of the comment to delete.</param>
    public void DeleteComment(int id)
    {
        _commentRepository.DeleteComment(id);
    }
}
