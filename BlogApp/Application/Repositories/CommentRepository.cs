using Domain.Entities;
using Domain.Exeptions;
using Domain.IRepositories;
using Infrastructure.Data;

namespace Application.Repositories;

public class CommentRepository(AppDbContext context) : ICommentRepository
{
    private readonly AppDbContext context = context;

    public List<Comment> GetAll()
    {
        List<Comment> list = [.. context.Comments];
        if (list == null) { return []; }
        else { return list; }
    }
    public void AddComment(Comment comment)
    {
        context.Comments.Add(comment);
        context.SaveChanges();
    }

    public void DeleteComment(int id)
    {
        Comment c = context.Comments.FirstOrDefault(c => c.Id == id)!;
        if (c == null) { throw new CommentNotFound($"Comment with the Id : {id} Not Found"); }
        else { context.Comments.Remove(c); context.SaveChanges(); }
    }
}
