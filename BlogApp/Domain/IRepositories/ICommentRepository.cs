using System;
using Domain.Aggregates;

namespace Domain.IRepositories;

public interface ICommentRepository
{
    public void AddComment (Comment comment);
    public void UpdateComment (Comment comment);
    public void DeleteComment (Comment comment);

}
