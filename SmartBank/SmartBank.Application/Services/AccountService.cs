using SmartBank.BLL.Interfaces;
using SmartBank.DAL.Interfaces;
using SmartBank.DAL.Models;

namespace SmartBank.BLL.Services
{
    public class AccountService: IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public Account CreateNewAccount(string currency, User user)
        {
            var account = new Account
            {
                AmountOfMoney = 0,
                Currency = currency,
                User = user,
                UserId = user.Id,
                DateIn = DateTime.Now
            };

            return _accountRepository.AddAccount(account);
        }
    }
}
