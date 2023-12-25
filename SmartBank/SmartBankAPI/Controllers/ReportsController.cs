using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartBank.BLL.Dtos.ReportDtos;
using SmartBank.BLL.Helper.Constants;
using SmartBank.BLL.Interfaces;
using System.Security.Claims;

namespace SmartBank.API.Controllers
{
    [Route("report/")]
    [ApiController]
    //[Authorize(Roles = Roles.User)]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpPost, Route("create")]
        public IActionResult CreateReport([FromBody] AddReportDto addReportDto)
        {
            var result = _reportService.GenerateReport(addReportDto.UserId, addReportDto.DateFrom, addReportDto.DateTo);

            return Ok(result);
        }

        [HttpGet,Route("list")]
        public IActionResult GetList()
        {
            var result = _reportService.GetReports((ClaimsIdentity)User.Identity!);

            return Ok(result);
        }
    }
}
