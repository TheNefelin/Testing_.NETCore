using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.EntityFramework.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HunterController : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            return Ok("Naizu");
        }
    }
}
