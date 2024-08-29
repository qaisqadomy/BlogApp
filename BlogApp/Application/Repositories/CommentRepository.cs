using Domain.Entities;
using Domain.Exeptions;
using Domain.IRepositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Application.Repositories;

public class CommentRepository : ICommentRepository
{
    private readonly AppDbContext context;
    public CommentRepository(AppDbContext context)
    {
        this.context = context;
    }
    public List<Comment> GetAll(){
        List<Comment> list = [.. context.Comments.Include(u => u.Author)];
        if(list == null){return new List<Comment>();}
        else {return list;}
    }
    public void AddComment(Comment comment)
    {
        context.Comments.Add(comment);
        context.SaveChanges();
    }

    public void DeleteComment(int id)
    {
        Comment c = context.Comments.FirstOrDefault(c => c.Id == id)!;
        if (c == null) { throw new NotFound(""); }
        else {context.Comments.Remove(c); context.SaveChanges();}
    }

}
