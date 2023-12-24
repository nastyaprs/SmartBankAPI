using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartBank.BLL.Dtos.CategoryDtos;
using SmartBank.BLL.Helper.Constants;
using SmartBank.BLL.Interfaces;
using System.Security.Claims;

namespace SmartBank.API.Controllers
{
    [Route("user")]
    [ApiController]
    [Authorize(Roles =Roles.User)]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAccountService _accountService;

        public UserController(IUserService userService, IAccountService accountService)
        {
            _userService = userService;
            _accountService = accountService;
        }

        [HttpGet, Route("profile")]
        public IActionResult GetUserProfile()
        {
            var result = _userService.GetUserProfile((ClaimsIdentity)User.Identity!);

            return Ok(result);
        }

        [HttpGet, Route("category/list/{id}")]
        public IActionResult GetCategoriesUser(int id)
        {
            var result = _userService.GetCategoriesForUser(id);

            return Ok(result);
        }

        [HttpPost, Route("category/add/{id}")]
        public IActionResult AddNewCategory(int id, [FromBody] CategoryDto categoryDto)
        {
            _userService.AddNewUsersCategory(id, categoryDto.Name);

            return Ok();
        }

        [HttpGet, Route("account/list/{id}")]
        public IActionResult GetAccountsByUserId(int id)
        {
            var accounts = _accountService.GetUsersAccounts(id);

            return Ok(accounts);
        }

        [HttpGet, Route("account/details/{id}")] //accountId
        public IActionResult GetAccountDetails(int id)
        {
            var details = _accountService.GetAccountDetails(id);

            return Ok(details);
        }

        [HttpPost, Route("account/create/{id}")]
        public IActionResult CreateAccountForUser(int id)
        {
            _userService.CreateNewAccountWithCard(id);

            return Ok();
        }
    }
}
