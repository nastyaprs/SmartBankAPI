using SmartBank.DAL.Models;

namespace SmartBank.BLL.Interfaces.IRepositories
{
    public interface IUserRepository
    {
        Task AddUser(User user);
        User? GetUserByEmail(string email);
    }
}
