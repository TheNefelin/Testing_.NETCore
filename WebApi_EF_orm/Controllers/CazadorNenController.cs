using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BibliotecaDeClases.Models;
using WebApi_EF_orm.Connections;
using BibliotecaDeClases.DTOs;

namespace WebApi_EF_orm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CazadorNenController : ControllerBase
    {
        private readonly WebApiDbContext _context;

        public CazadorNenController(WebApiDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CazadorNenDTO>>> GetAll(CancellationToken cancellationToken)
        {
            var entities = await _context.CazadorNen.ToListAsync(cancellationToken);

            return entities.Select(entity => new CazadorNenDTO
            {
                Id_Cazador = entity.Id_Cazador,
                Id_Nen = entity.Id_Nen,
            }).ToList();
        }

        [HttpGet("{Id_Cazador},{Id_Nen}")]
        public async Task<ActionResult<CazadorNenDTO>> GetById(int Id_Cazador, int Id_Nen, CancellationToken cancellationToken)
        {
            var entity = await _context.CazadorNen.FirstOrDefaultAsync(t => t.Id_Cazador == Id_Cazador && t.Id_Nen == Id_Nen, cancellationToken);

            if (entity == null)
                return NotFound();

            return new CazadorNenDTO
            {
                Id_Cazador = entity.Id_Cazador,
                Id_Nen = entity.Id_Nen
            };
        }

        [HttpPost]
        public async Task<ActionResult<CazadorNenDTO>> Insert(CazadorNenDTO dto, CancellationToken cancellationToken)
        {
            var entity = new CazadorNenModel
            {
                Id_Cazador = dto.Id_Cazador,
                Id_Nen = dto.Id_Nen,
            };

            var existCazador = _context.Cazadores.Any(e => e.Id == dto.Id_Cazador);
            if (!existCazador)
                return NotFound(new { Msge = $"NotFound Id_Cazador = {dto.Id_Cazador}" });

            var existNen = _context.Nen.Any(e => e.Id == dto.Id_Nen);
            if (!existNen)
                return NotFound(new { Msge = $"NotFound Id_Nen = {dto.Id_Nen}" });

            var existCN = _context.CazadorNen.Any(e => e.Id_Cazador == dto.Id_Cazador && e.Id_Nen == dto.Id_Nen);
            if (existCN)
                return BadRequest(new { Msge = $"BadRequest Elemento ya Existe" });

            _context.CazadorNen.Add(entity);

            try
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateException)
            {
                var exist = _context.CazadorNen.Any(e => e.Id_Cazador == dto.Id_Cazador && e.Id_Nen == dto.Id_Nen);
                if (!exist)
                    return NotFound(new { Msge = $"NotFound Id_Cazador = {dto.Id_Cazador}, Id_Nen {dto.Id_Nen}" });
                else
                    throw;
            }

            return CreatedAtAction("GetById", new { dto.Id_Cazador, dto.Id_Nen }, dto);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(CazadorNenDTO dto, CancellationToken cancellationToken)
        {
            var entity = await _context.CazadorNen.FirstOrDefaultAsync(t => t.Id_Cazador == dto.Id_Cazador && t.Id_Nen == dto.Id_Nen, cancellationToken); ;
            if (entity == null)
            {
                return NotFound();
            }

            _context.CazadorNen.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return NoContent();
        }
    }
}
