using Microsoft.AspNetCore.Mvc;

namespace WebApi.EntityFramework.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NenController : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<IActionResult> Insert(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        public async Task<IActionResult> Update(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
