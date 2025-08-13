using Microsoft.AspNetCore.Mvc;
using GestaoEstagios.Api.Services;
using GestaoEstagios.Api.Models;

namespace GestaoEstagios.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _auth;
        public AuthController(IAuthService auth) { _auth = auth; }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto dto)
        {
            var user = _auth.ValidateCredentials(dto.Email, dto.Password);
            if (user == null) return Unauthorized(new { message = "Credenciais inválidas" });
            var token = _auth.GenerateJwt(user);
            return Ok(new { token });
        }
    }

    public record LoginDto(string Email, string Password);
}
