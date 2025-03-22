using Microsoft.AspNetCore.Mvc;
using ProjectApi.Models;
using ProjectApi.Services;
using System.Threading.Tasks;

namespace ProjectApi.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        // Регистрация пользователя
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user, [FromQuery] string role)
        {
            if (user == null || string.IsNullOrEmpty(role))
            {
                return BadRequest("Invalid user data or role.");
            }

            var result = await _authService.RegisterAsync(user, role);
            if (result)
            {
                return Ok(new { Message = "User registered successfully." });
            }

            return BadRequest("User registration failed.");
        }

        // Логин пользователя
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest("Username and password are required.");
            }

            var token = await _authService.LoginAsync(request.Username, request.Password);
            if (!string.IsNullOrEmpty(token))
            {
                return Ok(new { Token = token });
            }

            return Unauthorized("Invalid username or password.");
        }
    }
}