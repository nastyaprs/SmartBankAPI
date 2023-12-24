using SmartBank.DAL.Models;

namespace SmartBank.DAL.Interfaces
{
    public interface IExpenseRepository
    {
        Expense AddExpense(Expense expense);
    }
}
