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
    }
}
