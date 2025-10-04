using Microsoft.AspNetCore.Mvc;
using ServicesLayer.Services.UserService;
using ServicesLayer.Services.UserService.Dtos;

namespace TaskApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService; 
        public AccountController(
            IUserService userService
            )
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register (RegisterDto input)
        {
            var result = await _userService.RegisterAync(input);
            
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login (LoginDto input)
        {
            var result = await _userService.LoginAsync(input);
    
            return result.IsSuccess ? Ok(result) : result.Errors.Code == "404" ? NotFound(result) : BadRequest(result)  ;
        }

    }
}
