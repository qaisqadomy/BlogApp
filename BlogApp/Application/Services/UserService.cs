using Application.DTOs;
using Domain.Entities;
using Domain.IRepositories;

namespace Application.Services;

/// <summary>
/// Provides services for user management, including retrieval, registration, login, and update operations.
/// </summary>

public class UserService(IUserRepository userRepository)
{
    private readonly IUserRepository _userRepository = userRepository;

    /// <summary>
    /// Retrieves user information based on the provided token.
    /// </summary>

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

    public string Login(string email, string password)
    {
        return _userRepository.Login(email, password);
    }

    /// <summary>
    /// Registers a new user with the provided information.
    /// </summary>

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
