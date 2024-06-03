using BibliotecaDeClases.DTOs;
using Microsoft.AspNetCore.Mvc;
using WebApi_Dapper.Connections;

namespace WebApi_Dapper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NenController : ControllerBase
    {
        private readonly DapperDbContext _dbContext;

        public NenController(DapperDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IEnumerable<NenDTO_Get>> GetAll(CancellationToken cancellationToken)
        {
            var result = await _dbContext.Nen_GetAll(cancellationToken);
            return result;
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<NenDTO_Get>> GetById(int Id, CancellationToken cancellationToken)
        {
            var result = await _dbContext.Nen_GetById(Id, cancellationToken);
            if (result == null)
                return NotFound();

            return result;
        }

        [HttpPost]
        public async Task<ActionResult<NenDTO_Get>> Insert(NenDTO_PostPut dto, CancellationToken cancellationToken)
        {
            int Id = await _dbContext.Nen_Insert(dto, cancellationToken);
            if (Id == 0)
                return BadRequest();

            var result = await _dbContext.Nen_GetById(Id, cancellationToken);
            if (result == null)
                return NotFound();

            return result;
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<NenDTO_Get>> Update(int Id, NenDTO_PostPut dto, CancellationToken cancellationToken)
        {
            await _dbContext.Nen_Update(Id, dto, cancellationToken);

            var result = await _dbContext.Nen_GetById(Id, cancellationToken);
            if (result == null)
                return NotFound();

            return result;
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id, CancellationToken cancellationToken)
        {
            await _dbContext.Nen_Delete(Id, cancellationToken);

            return NoContent();
        }
    }
}
