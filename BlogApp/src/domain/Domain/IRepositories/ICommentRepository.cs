using Domain.Entities;

namespace Domain.IRepositories
{
    /// <summary>
    /// Defines the interface for the repository responsible for managing comments.
    /// </summary>
    public interface ICommentRepository
    {
        /// <summary>
        /// Retrieves all comments from the repository.
        /// </summary>
        List<Comment> GetAll();

        /// <summary>
        /// Adds a new comment to the repository.
        /// </summary>
        void AddComment(Comment comment);

        /// <summary>
        /// Deletes a comment from the repository.
        /// </summary>
        void DeleteComment(int id);
    }
}
