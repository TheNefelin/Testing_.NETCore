using BibliotecaDeClases.DTOs;
using Microsoft.AspNetCore.Mvc;
using WebApi_DapperService.Services;

namespace WebApi_DapperService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CazadorController : ControllerBase
    {
        private readonly IBaseService<CazadorDTO_Get, CazadorDTO_PostPut> _service;

        public CazadorController(IBaseService<CazadorDTO_Get, CazadorDTO_PostPut> service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CazadorDTO_Get>>> GetAll(CancellationToken cancellationToken)
        {
            var entities = await _service.GetAll(cancellationToken);
            if (entities == null)
                return NotFound();
           
            return Ok(entities);
        }
    }
}
