using Application.DTOs;
using Domain.Entities;
using Domain.IRepositories;

namespace Application.Services;

public class UserService(IUserRepository userRepository)
{
    private readonly IUserRepository userRepository = userRepository;

    public GetUserDTO Get(string token)
    {
        User user = userRepository.Get(token);
        GetUserDTO userDTO = new() { UserName = user.UserName , Email = user.Email};
        return userDTO;
    }

    public string Login(string email, string password)
    {
        return userRepository.Login(email,password);
    }

    public void Register(UserDTO user)
    { 
        User user1 = new() { UserName = user.UserName ,Email = user.Email,Password = user.Password};
        userRepository.Register(user1);
    }

    public void Update(UserDTO user, string token)
    {
        User user1 = new() { UserName = user.UserName ,Email = user.Email,Password = user.Password};
        userRepository.Update(user1,token);
      
    }
}
