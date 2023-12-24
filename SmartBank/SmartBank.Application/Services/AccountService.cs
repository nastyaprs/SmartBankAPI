using SmartBank.BLL.Dtos.AccountDtos;
using SmartBank.BLL.Dtos.ExpenseDtos;
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

        public List<AccountDto> GetUsersAccounts(int userId)
        {
            var accountsList = _accountRepository.GetAccountsByUserId(userId);

            var list = new List<AccountDto>();
            foreach(var account in accountsList)
            {
                list.Add(new AccountDto()
                {
                    Id = account.Id,
                    AmountOfMoney = account.AmountOfMoney,
                    Currency = account.Currency,
                    DateIn = account.DateIn
                });
            }

            return list;
        }

        public AccountDetailsDto GetAccountDetails(int accountId)
        {
            var account = _accountRepository.GetAccountById(accountId);

            var details = new AccountDetailsDto()
            {
                Id = account.Id,
                AmountOfMoney = account.AmountOfMoney,
                Currency = account.Currency,
                DateIn = account.DateIn,
                CardNumber = account.Card.Number,
                UserId = account.UserId
            };

            foreach(var expense in account.Card.Expense)
            {
                details.Expenses.Add(new ExpenseDto()
                {
                    Id = expense.Id,
                    DateIn = expense.DateIn,
                    CategoryName = expense.Category.CategoryName,
                    Money = expense.Money
                });
            }

            return details;
        }

        public void AddMoney(AccountMoneyDto accountMoneyDto)
        {
            var account = _accountRepository.GetAccountById(accountMoneyDto.Id);

            account.AmountOfMoney += accountMoneyDto.AmountOfMoneyToAdd;

            _accountRepository.SaveChanges();
        }
    }
}
