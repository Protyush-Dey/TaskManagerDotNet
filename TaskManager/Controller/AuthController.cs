using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Dtos;
using TaskManager.Services;
using TaskManager.Utils;

namespace TaskManager.Controller
{
    [Route("api/Auth")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly IConfiguration _configuration;

        public AuthController(AuthService authService, IConfiguration configuration)
        {
            this._authService = authService;
            this._configuration = configuration;
        }

        [HttpPost("Register")]

        public async Task<IActionResult> Register(RegisterDto dto)
        {
            try
            {
                await _authService.Register(dto);
                return Ok(new ApiResponse<object>
                {
                    Success = true,
                    StatusCode = 200,
                    Message = "Register Successfully"

                });
            }
            catch (Exception ex) {
                return Ok(new ApiResponse<object>
                {
                    Success = false,
                    StatusCode = 409,
                    Message = ex.Message

                });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            try
            {
                string token = await _authService.Login(dto);
                return Ok(new ApiResponse<object>
                {
                    Success = true,
                    StatusCode = 200,
                    Message = "login Successfully",
                    Token = token,

                });
            }
            catch (Exception ex)
            {
                return Ok(new ApiResponse<object>
                {
                    Success = false,
                    StatusCode = 409,
                    Message = ex.Message

                });
            }
        }
        
        [Authorize(Roles = "Admin")]
        [HttpGet("Admin")]
        public IActionResult Admin()
        {
            return Ok(new
            {
                message = "Welcome Admin"
            });
        }

        [Authorize(Roles = "Employee")]
        [HttpGet("Employee")]
        public IActionResult Employee()
        {
            return Ok(new
            {
                message = "Welcome Employee"
            });
        }

        [Authorize(Roles = "Manager")]
        [HttpGet("Manager")]
        public IActionResult Manager()
        {
            return Ok(new
            {
                message = "Welcome Manager"
            });
        }
    }
}
