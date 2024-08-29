using Domain.Entities;

namespace Domain.IRepositories;

public interface ICommentRepository
{
    public List<Comment> GetAll();
    public void AddComment (Comment comment);
    public void DeleteComment (int id);

}
