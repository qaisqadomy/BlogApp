using Domain.Entities;

namespace Domain.IRepositories;

public interface ICommentRepository
{
    public void AddComment (Comment comment);
    public void UpdateComment (Comment comment ,int id);
    public void DeleteComment (int id);

}
