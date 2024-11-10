using ClassLibrary_SignalIR.DTOs;
using ClassLibrary_SignalIR.Models;
using ClassLibrary_SignalIR.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebApi_SignalR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserRepository _userService;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
            _userService = new UserRepository();
        }

        [HttpPost]
        public async Task<ActionResult<ApiResultDTO>> Login(LoginDTO login)
        {
            var user = _userService.Login(login);

            if (user != null)
            {
                var apiResult = new ApiResultDTO()
                {
                    Token = GenerateJwtToken(user)
                };

                return Ok(apiResult);
            }
            else
                return Unauthorized(); 
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, "Test"), 
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
