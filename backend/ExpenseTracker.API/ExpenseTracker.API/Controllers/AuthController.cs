using ExpenseTracker.API.Dtos;
using ExpenseTracker.API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var result = await _userService.RegisterAsync(request);

            if (!result.IsSuccess)
                return BadRequest(new { message = result.Error });

            return Created("", new { message = "User registered successfully" });
        }
    }
}
