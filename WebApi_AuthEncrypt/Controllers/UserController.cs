using Microsoft.AspNetCore.Mvc;
using WebApi_AuthEncrypt.Models.Dtos;

namespace WebApi_AuthEncrypt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        [HttpPost]
        [Route("register")]
        public async Task<string> Register(RegisterDTO dto)
        {
            return "HOLA";
        }

        [HttpPost]
        [Route("login")]
        public async Task<string> Login(LoginDTO dto) {
            return "HOLA";
        }
    }
}
