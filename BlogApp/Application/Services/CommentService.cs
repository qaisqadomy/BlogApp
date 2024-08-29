using Application.DTOs;
using Domain.Entities;
using Domain.IRepositories;

namespace Application.Services;

public class CommentService
{
    private readonly ICommentRepository commentRepository;
    public CommentService(ICommentRepository commentRepository)
    {
        this.commentRepository = commentRepository;

    }
    public List<CommentDTO> GetAll()
    {
        List<Comment> list = commentRepository.GetAll();
        return list.ConvertAll(comment => new CommentDTO
        {
            Body = comment.Body,
            CreatedAt = comment.CreatedAt,
            UpdatedAt = comment.UpdatedAt,
            Author = new UserDataDTO
            {
                UserName = comment.Author.UserName,
                Email = comment.Author.Email,
            }
        });
    }

    public void AddComment(CommentDTO comment)
    {
        Comment comment1 = new Comment
        {
            Body = comment.Body,
            CreatedAt = comment.CreatedAt,
            UpdatedAt = comment.UpdatedAt,
            Author = new User
            {
                UserName = comment.Author.UserName,
                Email = comment.Author.Email,
                Password = ""
            }
        };
        commentRepository.AddComment(comment1);

    }
    public void DeleteComment(int id)
    {
        commentRepository.DeleteComment(id);
    }


}
