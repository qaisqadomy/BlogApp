namespace Domain.IRepositories;

public interface IUserRepository
{
    public void Register(string userName, string email , string password);
    public void Login(string email, string password);
    
}
