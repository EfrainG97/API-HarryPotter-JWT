using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using PruebaHP.Model;
using System.Text.RegularExpressions;

namespace PruebaHP.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private const string AllowedDomain = "@alumnouninter.mx";

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            if (!IsValidEmail(request.Correo))
            {
                return BadRequest(new { message = "El correo electrónico no es válido o no pertenece al dominio permitido." });
            }

            // 🔐 Simulación de autenticación (puedes validar en una DB)
            if (request.Password == "123")
            {
                var secretKey = _configuration["JwtSettings:SecretKey"];
                var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
                new Claim(ClaimTypes.Name, request.Correo),
                new Claim(ClaimTypes.Role, "User")
            };

                var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.UtcNow.AddHours(1),
                    signingCredentials: creds
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                return Ok(new { token = tokenString });
            }

            return Unauthorized();
        }

        private bool IsValidEmail(string? email)
        {
            if (string.IsNullOrEmpty(email)) return false;

            // Expresión regular para validar formato de correo
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!Regex.IsMatch(email, emailPattern)) return false;

            // Verificar que el correo pertenezca al dominio permitido
            return email.EndsWith(AllowedDomain, StringComparison.OrdinalIgnoreCase);
        }

        public class LoginRequest
        {
            public string? Correo { get; set; }
            public string? Password { get; set; }
        }
    }
}
