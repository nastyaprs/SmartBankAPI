using SmartBank.DAL.Models;

namespace SmartBank.BLL.Interfaces
{
    public interface IAccountService
    {
        Account CreateNewAccount(string currency, User user);
    }
}
