using ClassLibrary.Models.DTOs;
using ClassLibrary.ServicesServer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.EntityFramework.Controllers
{
    [Route("api/hunter_nen")]
    [ApiController]
    public class HunterNenController : ControllerBase
    {
        public readonly ISimpleCRUD<HunterNenDTO> _service;

        public HunterNenController(ISimpleCRUD<HunterNenDTO> service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponseDTO<IEnumerable<HunterNenDTO>>>> GetAll(CancellationToken cancellationToken) {
            var result = await _service.GetAllAsync(cancellationToken);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut]
        public async Task<ActionResult<ApiResponseDTO<HunterNenDTO>>> Create(HunterNenDTO hunterNenDTO, CancellationToken cancellationToken)
        {
            var result = await _service.CreateAsync(hunterNenDTO, cancellationToken);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete]
        public async Task<ActionResult<ApiResponseDTO<HunterNenDTO>>> Delete(HunterNenDTO hunterNenDTO, CancellationToken cancellationToken)
        {
            var result = await _service.DeleteAsync(hunterNenDTO, cancellationToken);
            return StatusCode(result.StatusCode, result);
        }
    }
}
