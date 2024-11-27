using ClassLibrary.Models.DTOs;
using ClassLibrary.ServicesServer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.EntityFramework.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IConfiguration _configuration;

        public AuthController(ITokenGenerator tokenGenerator, IConfiguration configuration)
        {
            _tokenGenerator = tokenGenerator;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> registerAsync(RegisterRequestDTO register, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        [HttpPost("login")]
        public async Task<ActionResult<TokenDTO>> LoginAsync(LoginRequestDTO login, CancellationToken cancellationToken)
        {
            if (!login.Email.Equals("a@a") || !login.Password.Equals("123456"))
            {
                return Unauthorized(new { message = "Usuario o Contraseña Incorrecta" });
            }

            var apiAuth = new ApiAuthDTO()
            {
                UserId = Guid.NewGuid(),
                UserName = login.Email,
                Email = login.Email,
                Key = _configuration["JWT:Key"]!,
                Issuer = _configuration["JWT:Issuer"]!,
                Audience = _configuration["JWT:Audience"]!,
                ExpiresMin = int.Parse(_configuration["JWT:ExpireMin"]!),
            };

            var token = _tokenGenerator.GenerateToken(apiAuth);

            return Ok(token);
        }
    }
}
