using SmartBank.BLL.Dtos.AccountDtos;
using SmartBank.DAL.Models;

namespace SmartBank.BLL.Interfaces
{
    public interface IAccountService
    {
        Account CreateNewAccount(string currency, User user);
        List<AccountDto> GetUsersAccounts(int userId);
        AccountDetailsDto GetAccountDetails(int accountId);

    }
}
