using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartBank.BLL.Dtos.UserDtos;
using SmartBank.BLL.Interfaces.IServices;

namespace SmartBank.API.Controllers
{
    [Route("auth/")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost, Route("register")]
        public async Task<IActionResult> Register([FromBody] NewUserDto newUser)
        {
            var userExists = _userService.UserExists(newUser.Email);

            if (userExists)
            {
                return BadRequest("Email is taken");
            }

            await _userService.Register(newUser);
            return Ok();
        }
    }
}
