﻿using BibliotecaDeClases.DTOs;
using Microsoft.AspNetCore.Mvc;
using WebApi_DapperService.Classes;
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
        public async Task<IApiActionResult<IEnumerable<CazadorDTO_Get>>> GetAll(CancellationToken cancellationToken)
        {
            return await _service.GetAll(cancellationToken);
        }

        [HttpGet("{id}")]
        public async Task<IApiActionResult<CazadorDTO_Get>> GetById(int id, CancellationToken cancellationToken)
        {
            return await _service.GetById(id, cancellationToken);
        }

        [HttpPost]
        public async Task<IApiActionResult<CazadorDTO_Get>> Insert(CazadorDTO_PostPut dto, CancellationToken cancellationToken)
        {
            return await _service.Insert(dto, cancellationToken);
        }

        [HttpPut("{id}")]
        public async Task<IApiActionResult<CazadorDTO_Get>> Update(int id, CazadorDTO_PostPut dto, CancellationToken cancellationToken)
        {
            return await _service.Update(id, dto, cancellationToken);
        }

        [HttpDelete("{id}")]
        public async Task<IApiActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            return await _service.Delete(id, cancellationToken);
        }
    }
}
