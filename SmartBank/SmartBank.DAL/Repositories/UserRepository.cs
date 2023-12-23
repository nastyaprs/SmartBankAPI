using Microsoft.EntityFrameworkCore;
using SmartBank.BLL.Interfaces.IRepositories;
using SmartBank.DAL.Data;
using SmartBank.DAL.Models;

namespace SmartBank.DAL.Repository
{
    public class UserRepository: IUserRepository
    {
        private readonly SmartBankDBContext _smartBankDBContext;

        public UserRepository(SmartBankDBContext smartBankDBContext)
        {
            _smartBankDBContext = smartBankDBContext;
        }

        public async Task AddUser(User user)
        {
            await _smartBankDBContext.User.AddAsync(user);
            await _smartBankDBContext.SaveChangesAsync();
        }

        public User? GetUserByEmail(string email)
        {
            return _smartBankDBContext.User
                .Include(u => u.Address)
                .FirstOrDefault(u => u.Email == email);
        }

        public List<User> GetUnverifiedUsers()
        {
            return _smartBankDBContext.User
                .Include(u => u.Address)
                .Where(u => u.IsVerified == false)
                .ToList();
        }

        public User GetUserById(int id)
        {
            return _smartBankDBContext.User.First(u => u.Id == id);
        }

        public void SaveChanges()
        {
            _smartBankDBContext.SaveChanges();
        }
    }
}
