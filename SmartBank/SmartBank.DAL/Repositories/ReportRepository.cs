using Microsoft.EntityFrameworkCore;
using SmartBank.DAL.Data;
using SmartBank.DAL.Interfaces;
using SmartBank.DAL.Models;

namespace SmartBank.DAL.Repositories
{
    public class ReportRepository: IReportRepository
    {
        private readonly SmartBankDBContext _smartBankDBContext;

        public ReportRepository(SmartBankDBContext smartBankDBContext)
        {
            _smartBankDBContext = smartBankDBContext;
        }

        public Report AddReport(Report report)
        {
            _smartBankDBContext.Report.Add(report);
            _smartBankDBContext.SaveChanges();

            return report;
        }

        public Dictionary<string,Dictionary<string,List<Expense>>> GetAllUserExpencesByDate(int userId, DateTime dateFrom, DateTime dateTo)
        {
            return _smartBankDBContext.Expense
             .Include(e=> e.Category)
            .Include(e => e.Card)
            .ThenInclude(c => c.Account)
            .ThenInclude(a => a.User)
            .Where(e => e.Card.Account.UserId == userId
                    && e.DateIn >= dateFrom
                    && e.DateIn <= dateTo)
            .GroupBy(e => e.Card.Account.Currency)
            .ToDictionary(
                group => group.Key,
                group => group
                .GroupBy(e => e.Category.CategoryName)
                .ToDictionary(
                   group => group.Key,
                   group => group.ToList()));
        }

        public List<Report> GetUserReportsById(int userId)
        {
            return _smartBankDBContext.Report.Where(r => r.UserId == userId).ToList();
        }
    }
}
