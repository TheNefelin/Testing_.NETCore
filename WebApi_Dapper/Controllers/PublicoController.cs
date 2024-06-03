using BibliotecaDeClases.DTOs;
using Microsoft.AspNetCore.Mvc;
using WebApi_Dapper.Connections;

namespace WebApi_Dapper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicoController : ControllerBase
    {
        private readonly DapperDbContext _dbContext;
        public PublicoController(DapperDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("PorCazador")]
        public async Task<IEnumerable<RepoCazadorDTO>> CazadorGetAll(CancellationToken cancellationToken)
        {
            var cazadores = await _dbContext.Cazadores_GetAll(cancellationToken);
            var cazador_nen = await _dbContext.CazadorNen_GetAll(cancellationToken);
            var nen = await _dbContext.Nen_GetAll(cancellationToken);

            var result = cazadores.Select(c => new RepoCazadorDTO
            {
                Id = c.Id,
                Nombre = c.Nombre,
                Nen = cazador_nen.Where(cn => cn.Id_Cazador == c.Id)
                    .SelectMany(cn => nen.Where(n => n.Id == cn.Id_Nen)
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

        [HttpGet]
        [Route("PorNen")]
        public async Task<IEnumerable<RepoNenDTO>> NenGetAll(CancellationToken cancellationToken)
        {
            var cazadores = await _dbContext.Cazadores_GetAll(cancellationToken);
            var cazador_nen = await _dbContext.CazadorNen_GetAll(cancellationToken);
            var nen = await _dbContext.Nen_GetAll(cancellationToken);

            var result = nen.Select(n => new RepoNenDTO
            {
                Id = n.Id,
                Nombre = n.Nombre,
                Descripcion = n.Descripcion,
                Cazadores = cazador_nen.Where(cn => cn.Id_Nen == n.Id)
                    .SelectMany(cn => cazadores.Where(c => c.Id == cn.Id_Cazador)
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
