using SmartBank.DAL.Models;

namespace SmartBank.DAL.Interfaces
{
    public interface IReportRepository
    {
        Report AddReport(Report report);

        Dictionary<string, Dictionary<string, List<Expense>>> 
            GetAllUserExpencesByDate(int userId, DateTime dateFrom, DateTime dateTo);

        List<Report> GetUserReportsById(int userId);
    }
}
