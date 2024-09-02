using Domain.Entities;
using Domain.Exeptions;
using Domain.IRepositories;
using Infrastructure.Data;

namespace Application.Repositories;

/// <summary>
/// Provides data access methods for managing comments in the database.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="CommentRepository"/> class.
/// </remarks>
/// <param name="context">The <see cref="AppDbContext"/> used for database operations.</param>
public class CommentRepository(AppDbContext context) : ICommentRepository
{
    private readonly AppDbContext _context = context;

    /// <summary>
    /// Retrieves all comments from the database.
    /// </summary>
    /// <returns>A list of all comments.</returns>
    public List<Comment> GetAll()
    {
        List<Comment> list = _context.Comments.ToList();
        return list;
    }

    /// <summary>
    /// Adds a new comment to the database.
    /// </summary>
    /// <param name="comment">The comment to add.</param>
    public void AddComment(Comment comment)
    {
        _context.Comments.Add(comment);
        _context.SaveChanges();
    }

    /// <summary>
    /// Deletes a comment from the database.
    /// </summary>
    /// <param name="id">The ID of the comment to delete.</param>
    /// <exception cref="CommentNotFound">Thrown when the comment with the specified ID is not found.</exception>
    public void DeleteComment(int id)
    {
        Comment comment = _context.Comments.FirstOrDefault(c => c.Id == id)
            ?? throw new CommentNotFound($"Comment with the Id : {id} not found");

        _context.Comments.Remove(comment);
        _context.SaveChanges();
    }
}
