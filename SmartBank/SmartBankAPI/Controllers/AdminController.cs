using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartBank.BLL.Helper.Constants;
using SmartBank.BLL.Interfaces;

namespace SmartBank.API.Controllers
{
    [Route("admin/")]
    [ApiController]
    [Authorize(Roles = Roles.Admin)]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet, Route("user/list")]
        public IActionResult GetListOfUnverifiedUsers()
        {
            var usersList = _adminService.GetUnverifiedUsers();

            return Ok(usersList);
        }

        [HttpPut, Route("user/verify/{id}")]
        public IActionResult VerifyUser(int id)
        {
            _adminService.VerifyUser(id);

            return Ok();
        }
    }
}
