using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using EmployeeAPI.Data;
using EmployeeAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmployeeAPI.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;

        public AuthController(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        // REGISTER
        [HttpPost("register")]
        public async Task<IActionResult> Register(User user)
        {
            var exists = _context.Users.Any(x => x.Email == user.Email);

            if (exists)
                return BadRequest("User already exists");

            // Hash password
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok("User Registered Successfully");
        }

        // LOGIN
        [HttpPost("login")]
        public IActionResult Login(User login)
        {
            var user = _context.Users
                        .FirstOrDefault(x => x.Email == login.Email);

            if (user == null)
                return Unauthorized("Invalid credentials");

            // Verify password
            bool isValid = BCrypt.Net.BCrypt.Verify(
                login.PasswordHash,
                user.PasswordHash
            );

            if (!isValid)
                return Unauthorized("Invalid credentials");

            // Generate JWT
            var token = GenerateToken(user);

            return Ok(new { token });
        }

        private string GenerateToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["JwtSettings:Key"])
            );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["JwtSettings:Issuer"],
                audience: _config["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(
                    Convert.ToDouble(_config["JwtSettings:DurationInMinutes"])
                ),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
