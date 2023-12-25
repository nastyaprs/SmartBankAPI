using Newtonsoft.Json;
using SmartBank.BLL.Dtos.ReportDtos;
using SmartBank.BLL.Interfaces;
using SmartBank.DAL.Interfaces;
using SmartBank.DAL.Models;
using System.Security.Claims;

namespace SmartBank.BLL.Services
{
    public class ReportService: IReportService
    {
        private readonly IReportRepository _reportRepository;
        private readonly IUserService _userService;

        public ReportService(IReportRepository reportRepository, IUserService userService)
        {
            _reportRepository = reportRepository;
            _userService = userService;
        }

        public ReportDto GenerateReport(int userId, DateTime dateFrom, DateTime dateTo)
        {
            Report report = new Report
            {
                UserId = userId,
                DateIn = DateTime.Now,
                DateFrom = dateFrom,
                DateTo = dateTo
            };

            var expences = _reportRepository.GetAllUserExpencesByDate(userId, dateFrom, dateTo);

            foreach (var (currency, categoryExpenses) in expences)
            {
                Dictionary<string, decimal> categoryMoney = new Dictionary<string, decimal>();

                foreach (var (category, categoryExpensesList) in categoryExpenses)
                {
                    decimal categoryTotalAmount = categoryExpensesList.Sum(expense => expense.Money);

                    report.User = categoryExpensesList.First().Card.Account.User;

                    categoryMoney.Add(category, categoryTotalAmount);
                }

                UpdateCategoryDiagram(report, currency, categoryMoney);
            }

            report = _reportRepository.AddReport(report);


            return new ReportDto()
            {
                Id = report.Id,
                DateIn = report.DateIn,
                UserId = report.UserId,
                Content = report.Content
            };
        }

        private void UpdateCategoryDiagram(Report report, string currency, Dictionary<string, decimal> categoryAmounts)
        {
            report.Content += $"<p>Currency: {currency}</p>";

            report.Content += $@"
    <canvas id='chart_{currency}' width='300' height='300'></canvas>
    <script src='https://cdn.jsdelivr.net/npm/chart.js'></script>
    <script>
        var ctx = document.getElementById('chart_{currency}').getContext('2d');
        var myChart = new Chart(ctx, {{
            type: 'doughnut',
            data: {{
                labels: {JsonConvert.SerializeObject(categoryAmounts.Keys)},
                datasets: [{{
                    data: {JsonConvert.SerializeObject(categoryAmounts.Values)},
                    backgroundColor: ['#FF6384', '#36A2EB', '#FFCE56', '#8B4513', '#008080'] 
                }}]
            }}
        }});
    </script>";

        }

        public List<ReportDto> GetReports(ClaimsIdentity claimsIdentity)
        {
            var user = _userService.GetUserByToken(claimsIdentity);

            var reportRaw = _reportRepository.GetUserReportsById(user.Id);

            var list = new List<ReportDto>();
            foreach(var report in reportRaw)
            {
                list.Add(new ReportDto()
                {
                    Id = report.Id,
                    UserId = report.UserId,
                    DateIn = report.DateIn,
                    Content = report.Content,
                    DateFrom = report.DateFrom,
                    DateTo = report.DateTo
                });
            }

            return list;
        }
    }
}
