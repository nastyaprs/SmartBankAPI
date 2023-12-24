using SmartBank.DAL.Data;
using SmartBank.DAL.Interfaces;
using SmartBank.DAL.Models;

namespace SmartBank.DAL.Repositories
{
    public class ExpenseRepository: IExpenseRepository
    {
        private readonly SmartBankDBContext _smartBankDBContext;

        public ExpenseRepository(SmartBankDBContext smartBankDBContext)
        {
            _smartBankDBContext = smartBankDBContext;
        }

        public Expense AddExpense(Expense expense)
        {
            _smartBankDBContext.Expense.Add(expense);
            _smartBankDBContext.SaveChanges();

            return expense;
        }
    }
}
