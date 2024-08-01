using BibliotecaDeClases.DTOs;
using BibliotecaDeClases.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApi_AuthJWT.Connections;

namespace WebApi_AuthJWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly WebApiDbContext _dapper;

        public AuthController(IConfiguration configuration, WebApiDbContext dapper)
        {
            _configuration = configuration;
            _dapper = dapper;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult<AuthResponseDTO>> register(AuthRegisterDTO dto, CancellationToken cancellationToken)
        {
            AuthRegister login = new AuthRegister()
            {
                Id = Guid.NewGuid().ToString(),
                Email = dto.Email,
                Password = dto.Password,
                PasswordConfirmed = dto.PasswordConfirmed,
            };

            var resp = await _dapper.AuthRegister(login, cancellationToken);
                   
            return resp;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<AuthResponseDTO>> login(AuthLoginDTO dto, CancellationToken cancellationToken)
        {
            AuthResponseDTO resp = await _dapper.AuthLogin(dto, cancellationToken);

            if (resp.StatusCode == 201) 
            {
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]!));
                var issuer = _configuration["JWT:Issuer"];
                var audience = _configuration["JWT:Audience"];
                var expire = _configuration["JWT:ExpireMin"];
                String TokenId = Guid.NewGuid().ToString();

                var tokenOptions = new JwtSecurityToken(
                           issuer: issuer,
                           audience: audience,
                           claims: new List<Claim> {
                                new (JwtRegisteredClaimNames.Jti, TokenId),
                                new (JwtRegisteredClaimNames.Sub, dto.Email),
                                new (JwtRegisteredClaimNames.Email, dto.Email),
                           },
                           expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(expire)),
                           signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                       );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                return Ok(new { resp.StatusCode, resp.StatusMessage, Token = tokenString });


            }

            return resp;
            //return Unauthorized();
        }

    }
}
