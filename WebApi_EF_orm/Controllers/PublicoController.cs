using BibliotecaDeClases.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using WebApi_EF_orm.Connections;

namespace WebApi_EF_orm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicoController : ControllerBase
    {
        private readonly WebApiDbContext _context;

        public PublicoController(WebApiDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("Cazadores")]
        public async Task<ActionResult<IEnumerable<dynamic>>> Cazadores_GetAll(CancellationToken cancellationToken)
        {
            var entities = await _context.Cazadores
                .Include(t => t.Cazador_Nen)
                    .ThenInclude(t => t.Nen)
                .ToListAsync(cancellationToken);


            return entities.Select(c => new RepoCazadorDTO
            {
                Id = c.Id,
                Edad = c.Edad,
                Nen = c.Cazador_Nen
                    .Where(cn => cn.Id_Cazador == c.Id)
                    .Select(cn => new NenDTO_Get
                    {
                        Id = cn.Nen.Id,
                        Nombre = cn.Nen.Nombre,
                        Descripcion = cn.Nen.Descripcion,
                    }).ToList(),
            }).ToList();
        }

        [HttpGet]
        [Route("Nen")]
        public async Task<ActionResult<IEnumerable<dynamic>>> Nen_GetAll(CancellationToken cancellationToken)
        {
            var entities = await _context.Nen
                .Include(t => t.Cazador_Nen)
                    .ThenInclude(t => t.Cazador)
                .ToListAsync(cancellationToken);


            return entities.Select(c => new RepoNenDTO
            {
                Id = c.Id,
                Descripcion = c.Descripcion,
                Cazadores = c.Cazador_Nen
                    .Where(cn => cn.Id_Nen == c.Id)
                    .Select(cn => new CazadorDTO_Get
                    {
                        Id = cn.Cazador.Id,
                        Nombre = cn.Cazador.Nombre,
                        Edad = cn.Cazador.Edad,
                    }).ToList(),
            }).ToList();
        }
    }
}
