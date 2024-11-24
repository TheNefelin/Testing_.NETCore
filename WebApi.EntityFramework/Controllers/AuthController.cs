using ClassLibrary.ServicesServer.DTOs;
using ClassLibrary.ServicesServer.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.EntityFramework.Controllers
{
    [Route("api/[controller]")]
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

        [HttpPost]
        public async Task<ActionResult<TokenDTO>> LoginAsync(LoginRequestDTO login, CancellationToken cancellationToken)
        {
            if (!login.Email.Equals("a@a") || !login.Password.Equals("123"))
            {
                return Unauthorized(new { message = "Usuario o Contraseña Incorrecta" });
            }

            var auth = new AuthDTO()
            {
                UserId = "ABC123",
                UserName = login.Email,
                Email = login.Email,
                Key = _configuration["JWT:Key"]!,
                Issuer = _configuration["JWT:Issuer"]!,
                Audience = _configuration["JWT:Audience"]!,
                ExpiresMin = int.Parse(_configuration["JWT:ExpireMin"]!),
            };

            var token = _tokenGenerator.GenerateToken(auth);

            return Ok(token);
        }
    }
}
