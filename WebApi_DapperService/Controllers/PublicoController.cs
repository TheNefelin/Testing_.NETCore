using BibliotecaDeClases.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi_DapperService.Services;

namespace WebApi_DapperService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicoController : ControllerBase
    {
        private readonly IBaseService<CazadorDTO_Get, CazadorDTO_PostPut> _cazadorService;
        private readonly IBaseService<CazadorNenDTO> _cazadorNenService;
        private readonly IBaseService<NenDTO_Get, NenDTO_PostPut> _nenService;

        public PublicoController(
            IBaseService<CazadorDTO_Get, CazadorDTO_PostPut> cazadorService, 
            IBaseService<CazadorNenDTO> cazadorNenService, 
            IBaseService<NenDTO_Get, NenDTO_PostPut> nenService)
        {
            _cazadorService = cazadorService;
            _cazadorNenService = cazadorNenService;
            _nenService = nenService;
        }

        [HttpGet("PorCazador")]
        public async Task<IEnumerable<RepoCazadorDTO>> CazadorGetAll(CancellationToken cancellationToken)
        {
            var taskCazadores = _cazadorService.GetAll(cancellationToken); ;
            var taskCazadorNen = _cazadorNenService.GetAll(cancellationToken);
            var taskNen = _nenService.GetAll(cancellationToken);

            Task.WaitAll(taskCazadores, taskCazadorNen, taskNen);

            var cazadores = await taskCazadores;
            var cazador_nen = await taskCazadorNen;
            var nen = await taskNen;

            var result = cazadores.Data is null ? [] : cazadores.Data.Select(c => new RepoCazadorDTO
            {
                Id = c.Id,
                Nombre = c.Nombre,
                Nen = cazador_nen.Data is null ? [] : cazador_nen.Data.Where(cn => cn.Id_Cazador == c.Id)
                    .SelectMany(cn => nen.Data.Where(n => n.Id == cn.Id_Nen)
                        .Select(n => new NenDTO_Get
                        {
                            Id = n.Id,
                            Nombre = n.Nombre,
                            Descripcion = n.Descripcion,
                        })
                    ).ToList(),
            }).ToList();

            return result;
        }

        [HttpGet("PorNen")]
        public async Task<IEnumerable<RepoNenDTO>> NenGetAll(CancellationToken cancellationToken)
        {
            var taskNen = _nenService.GetAll(cancellationToken);
            var taskCazadorNen = _cazadorNenService.GetAll(cancellationToken);
            var taskCazadores = _cazadorService.GetAll(cancellationToken);

            Task.WaitAll(taskNen, taskCazadorNen, taskCazadores);

            var nen = await taskNen;
            var cazador_nen = await taskCazadorNen;
            var cazadores = await taskCazadores;

            var result = nen.Data is null ? [] : nen.Data.Select(n => new RepoNenDTO
            {
                Id = n.Id,
                Nombre = n.Nombre,
                Descripcion = n.Descripcion,
                Cazadores = cazador_nen.Data is null ? [] : cazador_nen.Data.Where(cn => cn.Id_Nen == n.Id)
                    .SelectMany(cn => cazadores.Data is null ? [] : cazadores.Data.Where(c => c.Id == cn.Id_Cazador)
                        .Select(c => new CazadorDTO_Get
                        {
                            Id = c.Id,
                            Nombre = c.Nombre,
                            Edad = c.Edad,
                        })
                    ).ToList(),
            }).ToList();

            return result;
        }
    }
}
