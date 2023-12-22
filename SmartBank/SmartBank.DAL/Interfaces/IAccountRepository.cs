using SmartBank.DAL.Models;

namespace SmartBank.DAL.Interfaces
{
    public interface IAccountRepository
    {
        Account AddAccount(Account account);
    }
}
