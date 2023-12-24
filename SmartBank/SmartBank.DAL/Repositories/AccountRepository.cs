using Microsoft.EntityFrameworkCore;
using SmartBank.DAL.Data;
using SmartBank.DAL.Interfaces;
using SmartBank.DAL.Models;

namespace SmartBank.DAL.Repositories
{
    public class AccountRepository: IAccountRepository
    {
        private readonly SmartBankDBContext _smartBankDBContext;

        public AccountRepository(SmartBankDBContext smartBankDBContext)
        {
            _smartBankDBContext = smartBankDBContext;
        }

        public Account AddAccount(Account account)
        {
            _smartBankDBContext.Account.Add(account);
            _smartBankDBContext.SaveChanges();

            return account;
        }

        public List<Account> GetAccountsByUserId(int userId)
        {
            return _smartBankDBContext.Account.Where(a => a.UserId == userId).ToList();
        }

        public Account GetAccountById(int id)
        {
            return _smartBankDBContext.Account
                .Include(a => a.Card)
                .ThenInclude(c=> c!.Expense)
                .ThenInclude(e => e.Category)
                .First(a => a.Id == id);
        }
    }
}
