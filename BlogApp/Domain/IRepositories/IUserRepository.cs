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
        /// <param name="user">The user to register.</param>
        void Register(User user);

        /// <summary>
        /// Logs in a user and returns a JWT token.
        /// </summary>
        /// <param name="email">The email of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <returns>A JWT token for the logged-in user.</returns>
        string Login(string email, string password);

        /// <summary>
        /// Retrieves a user based on the provided JWT token.
        /// </summary>
        /// <param name="token">The JWT token of the user.</param>
        /// <returns>The user associated with the token.</returns>
        /// <exception cref="InvalidToken">Thrown if the token is invalid.</exception>
        User Get(string token);

        /// <summary>
        /// Updates the user's information based on the provided JWT token.
        /// </summary>
        /// <param name="user">The user with updated information.</param>
        /// <param name="token">The JWT token of the user to update.</param>
        /// <exception cref="InvalidToken">Thrown if the token is invalid.</exception>
        void Update(User user, string token);

        /// <summary>
        /// Retrieves a list of users by their identifiers.
        /// </summary>
        /// <param name="AuthorId">The list of user identifiers.</param>
        /// <returns>A list of users associated with the provided identifiers.</returns>
        List<User> GetByIds(List<int> AuthorId);
    }
}
