﻿using ClassLibrary.Models.DTOs;
using ClassLibrary.Models.Entities;
using ClassLibrary.ServicesServer.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace WebApi.EntityFramework.Controllers
{
    [Route("api/hunter")]
    [ApiController]
    public class HunterController : ControllerBase
    {
        private readonly IServiceBaseCRUD<HunterDTO, Hunter> _service;

        public HunterController(IServiceBaseCRUD<HunterDTO, Hunter> service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HunterDTO>>> GetAll(CancellationToken cancellationToken)
        {
            var result = await _service.GetAllAsync(cancellationToken);

            if (result == null || !result.Any()) return NotFound();

            return Ok(result);
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
