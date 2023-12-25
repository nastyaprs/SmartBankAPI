using SmartBank.BLL.Dtos.ReportDtos;
using System.Security.Claims;

namespace SmartBank.BLL.Interfaces
{
    public interface IReportService
    {
        ReportDto GenerateReport(int userId, DateTime dateFrom, DateTime dateTo);
        List<ReportDto> GetReports(ClaimsIdentity claimsIdentity);
    }
}
