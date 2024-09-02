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
        /// <returns>A list of all comments.</returns>
        List<Comment> GetAll();

        /// <summary>
        /// Adds a new comment to the repository.
        /// </summary>
        /// <param name="comment">The comment to add.</param>
        void AddComment(Comment comment);

        /// <summary>
        /// Deletes a comment from the repository.
        /// </summary>
        /// <param name="id">The identifier of the comment to delete.</param>
        void DeleteComment(int id);
    }
}
