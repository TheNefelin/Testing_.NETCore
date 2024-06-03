using BibliotecaDeClases.DTOs;
using Microsoft.AspNetCore.Mvc;
using WebApi_Dapper.Connections;

namespace WebApi_Dapper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CazadorNenController : ControllerBase
    {
        private readonly DapperDbContext _dbContext;
        public CazadorNenController(DapperDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IEnumerable<CazadorNenDTO>> GetAll(CancellationToken cancellationToken)
        {
            var result = await _dbContext.CazadorNen_GetAll(cancellationToken);
            return result;
        }

        [HttpPost]
        public async Task<IActionResult> Insert(CazadorNenDTO dto, CancellationToken cancellationToken)
        {
            await _dbContext.CazadorNen_Insert(dto, cancellationToken);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(CazadorNenDTO dto, CancellationToken cancellationToken)
        {
            await _dbContext.CazadorNen_Delete(dto, cancellationToken);
            return NoContent();
        }
    }
}
