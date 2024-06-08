using BibliotecaDeClases.DTOs;
using Microsoft.AspNetCore.Mvc;
using WebApi_DapperService.Classes;
using WebApi_DapperService.Services;

namespace WebApi_DapperService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CazadorNenController : ControllerBase
    {
        private readonly IBaseService<CazadorNenDTO> _service;

        public CazadorNenController(IBaseService<CazadorNenDTO> service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IApiActionResult<IEnumerable<CazadorNenDTO>>> GetAll(CancellationToken cancellationToken)
        {
            return await _service.GetAll(cancellationToken);
        }

        [HttpPost]
        public async Task<IApiActionResult<CazadorNenDTO>> Insert(CazadorNenDTO dto, CancellationToken cancellationToken)
        {
            return await _service.Insert(dto, cancellationToken);
        }

        [HttpDelete]
        public async Task<IApiActionResult> Delete(CazadorNenDTO dto, CancellationToken cancellationToken)
        {
            return await _service.Delete(dto, cancellationToken);
        }
    }
}
