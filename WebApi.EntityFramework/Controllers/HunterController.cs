using ClassLibrary.Models.DTOs;
using ClassLibrary.ServicesServer.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace WebApi.EntityFramework.Controllers
{
    [Route("api/hunter")]
    [ApiController]
    public class HunterController : ControllerBase
    {
        private readonly IBaseCRUD<HunterDTO, HunterGetDTO> _service;

        public HunterController(IBaseCRUD<HunterDTO, HunterGetDTO> service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponseDTO<IEnumerable<HunterDTO>>>> GetAll(CancellationToken cancellationToken)
        {
            var response = await _service.GetAllAsync(cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponseDTO<HunterGetDTO>>> GetById(int id, CancellationToken cancellationToken)
        {
            var response = await _service.GetByIdAsync(id, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponseDTO<HunterDTO>>> Create(HunterDTO hunterDTO, CancellationToken cancellationToken)
        {
            var response = await _service.CreateAsync(hunterDTO, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponseDTO<HunterDTO>>> Update(int id, HunterDTO hunterDTO, CancellationToken cancellationToken)
        {
            var response = await _service.UpdateAsync(id, hunterDTO, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponseDTO<object>>> Delete(int id, CancellationToken cancellationToken)
        {
            var response = await _service.DeleteAsync(id, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
    }
}
