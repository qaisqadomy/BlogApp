using Domain.Entities;

namespace Domain.IRepositories
{
    /// <summary>
    /// Defines the interface for the repository responsible for managing users.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Registers a new user in the repository.
        /// </summary>
        void Register(User user);

        /// <summary>
        /// Logs in a user and returns a JWT token.
        /// </summary>
        string Login(string email, string password);

        /// <summary>
        /// Retrieves a user based on the provided JWT token.
        /// </summary>
        User Get(string token);

        /// <summary>
        /// Updates the user's information based on the provided JWT token.
        /// </summary>
        void Update(User user, string token);

        /// <summary>
        /// Retrieves a list of users by their identifiers.
        /// </summary>
        List<User> GetByIds(List<int> AuthorId);
    }
}
