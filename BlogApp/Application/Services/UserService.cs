using Application.DTOs;
using Domain.Entities;
using Domain.IRepositories;

namespace Application.Services;

/// <summary>
/// Provides services for user management, including retrieval, registration, login, and update operations.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="UserService"/> class.
/// </remarks>
/// <param name="userRepository">The <see cref="IUserRepository"/> used for user data operations.</param>
public class UserService(IUserRepository userRepository)
{
    private readonly IUserRepository _userRepository = userRepository;

    /// <summary>
    /// Retrieves user information based on the provided token.
    /// </summary>
    /// <param name="token">The JWT token used to identify the user.</param>
    /// <returns>A <see cref="GetUserDTO"/> representing the user's details.</returns>
    /// <exception cref="InvalidToken">Thrown if the provided token is invalid.</exception>
    /// <exception cref="UserNotFound">Thrown if no user is found for the given token.</exception>
    public GetUserDTO Get(string token)
    {
        User user = _userRepository.Get(token);
        return new GetUserDTO
        {
            UserName = user.UserName,
            Email = user.Email
        };
    }

    /// <summary>
    /// Logs in a user using their email and password.
    /// </summary>
    /// <param name="email">The email of the user.</param>
    /// <param name="password">The password of the user.</param>
    /// <returns>A JWT token for the authenticated user.</returns>
    /// <exception cref="NotRegesterd">Thrown if the user is not registered.</exception>
    public string Login(string email, string password)
    {
        return _userRepository.Login(email, password);
    }

    /// <summary>
    /// Registers a new user with the provided information.
    /// </summary>
    /// <param name="user">The <see cref="UserDTO"/> containing the user's details.</param>
    public void Register(UserDTO user)
    {
        User userEntity = new()
        {
            UserName = user.UserName,
            Email = user.Email,
            Password = user.Password
        };
        _userRepository.Register(userEntity);
    }

    /// <summary>
    /// Updates an existing user based on the provided information and token.
    /// </summary>
    /// <param name="user">The <see cref="UserDTO"/> containing the updated user details.</param>
    /// <param name="token">The JWT token of the user to update.</param>
    /// <exception cref="InvalidToken">Thrown if the provided token is invalid.</exception>
    /// <exception cref="UserNotFound">Thrown if no user is found for the given token.</exception>
    public void Update(UserDTO user, string token)
    {
        User userEntity = new()
        {
            UserName = user.UserName,
            Email = user.Email,
            Password = user.Password
        };
        _userRepository.Update(userEntity, token);
    }
}
