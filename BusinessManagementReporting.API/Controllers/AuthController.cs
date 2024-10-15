using BusinessManagementReporting.Core.DTOs.Auth;
using BusinessManagementReporting.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BusinessManagementReporting.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            _logger.LogInformation("User registration attempt.");

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid registration data.");
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _authService.RegisterAsync(model);
                _logger.LogInformation("User registered successfully.");
                return Ok(new { message = "User registered successfully." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "User registration failed.");
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            _logger.LogInformation("User login attempt.");

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid login data.");
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _authService.LoginAsync(model);
                _logger.LogInformation("User logged in successfully.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "User login failed.");
                return Unauthorized(new { message = ex.Message });
            }
        }
    }
}
