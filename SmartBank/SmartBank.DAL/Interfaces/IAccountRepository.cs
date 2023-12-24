using SmartBank.DAL.Models;

namespace SmartBank.DAL.Interfaces
{
    public interface IAccountRepository
    {
        Account AddAccount(Account account);
        List<Account> GetAccountsByUserId(int userId);
        Account GetAccountById(int id);
        void SaveChanges();
    }
}
