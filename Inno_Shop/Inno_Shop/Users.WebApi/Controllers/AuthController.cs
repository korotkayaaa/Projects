using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Users.Persistence;
using Users.Application.Services;
using Microsoft.AspNetCore.Identity;

namespace Users.WebApi.Controllers
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    }

    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
    }
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : BaseController
    {
        private readonly Inno_ShopDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly UserManager<IdentityUser> _userManager;

        public AuthController(Inno_ShopDbContext context, IConfiguration configuration, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _configuration = configuration;
            _userManager = userManager;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            //var rem =  _userManager.Users.FirstOrDefault(u => u.UserName == "test");
            //_context.Users.Remove(rem);
            var user = _context.Useres.SingleOrDefault(u => u.Name == model.Username);
            if (user != null)
            {
                var IsValidPassword = _userManager.PasswordHasher.VerifyHashedPassword(_context.Users.SingleOrDefault(u => u.UserName == model.Username), user.Password, model.Password); ;
                if (IsValidPassword == PasswordVerificationResult.Success || user.Password == model.Password)
                {
                    var claims = new[]
                    {
                new Claim(JwtRegisteredClaimNames.Sub, user.Name),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("id", user.Id.ToString())
                };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(
                        issuer: _configuration["Jwt:Issuer"],
                        audience: _configuration["Jwt:Audience"],
                        claims: claims,
                        expires: DateTime.Now.AddMinutes(30),
                        signingCredentials: creds);

                    return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
                }
            }

            return Unauthorized();
        }
    }
        public class LoginModel
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }
}


