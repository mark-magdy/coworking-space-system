using coworking_space.BAL.Interaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using coworking_space.BAL.DTOs;
using coworking_space.BAL.DTOs.UserDTO;
using coworking_space.BAL.Dtos.UserDTO;

namespace coworking_space.API.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService) {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto) {
            var token = await _authService.LoginAsync(dto.Username, dto.Password);
            if (token == null)
                return Unauthorized(new { message = "Invalid credentials" });

            return Ok(new { token });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserCreateDto dto) {
            var token = await _authService.RegisterAsync(dto);
            if (token == null)
                return BadRequest(new { message = "User already exists" });

            return Ok(new { token });
        }
    }
}
