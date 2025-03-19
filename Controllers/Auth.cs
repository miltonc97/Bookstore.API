using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Bookstore.API.Controllers
{
    [Route("auth")]
    [ApiController]
    public class Auth : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public Auth(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("login")]
        public IActionResult Login(string userName, string password)
        {
            // Validate user credentials (you should integrate with your database here)
            if (userName == "admin" && password == "password")
            {
                var token = GenerateJwtToken(userName);
                return Ok(new { Token = token });
            }

            return Unauthorized("Invalid credentials");
        }

        private string GenerateJwtToken(string username)
        {
            var claims = new[]
            {
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Role, "Admin"),  // Set the role as 'Admin' for this example
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your-secret-key-your-secret-key-secret"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "Milton",
                audience: "Bookstore",
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
