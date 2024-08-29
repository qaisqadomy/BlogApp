using Domain.Entities;

namespace Domain.IRepositories;

public interface IUserRepository
{
    public void Register(User user);
    public string Login(string email, string password);
    public User Get(string token);
    public void Update(User user, string token);
   public List<User>  GetByIds(List<int> AuthorId);
}
