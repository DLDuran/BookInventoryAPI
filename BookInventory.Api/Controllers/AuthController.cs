using Microsoft.AspNetCore.Mvc;
using BookInventory.Application.DTOs;
using BookInventory.Infrastructure.Data;
using BookInventory.Application.Interfaces.Services;

namespace BookInventory.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var response = await _authService.LoginAsync(request);

            if (response == null)
            {
                return NotFound("User not found with the assigned credentials");
            }
            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var response = await _authService.RegisterAsync(request);

            if (!response)
            {
                return BadRequest("Failed to register user");
            }
            return Ok(new { message = "Registration successful" });
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] TokenRequest request)
        {
            var response = await _authService.RefreshTokenAsync(request);

            if (response == null)
            {
                return BadRequest("Failed to refresh");
            }
            return Ok(response);
        }
    }
}