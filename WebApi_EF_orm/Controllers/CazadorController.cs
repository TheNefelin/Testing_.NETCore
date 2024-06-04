using BibliotecaDeClases.DTOs;
using BibliotecaDeClases.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi_EF_orm.Connections;

namespace WebApi_EF_orm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CazadorController : ControllerBase
    {
        private readonly WebApiDbContext _context;

        public CazadorController(WebApiDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CazadorDTO_Get>>> GetAll(CancellationToken cancellationToken)
        {
            var entities = await _context.Cazadores.ToListAsync(cancellationToken);

            return entities.Select(entity => new CazadorDTO_Get
            {
                Id = entity.Id,
                Nombre = entity.Nombre,
                Edad = entity.Edad,
            }).ToList();
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<CazadorDTO_Get>> GetById(int Id, CancellationToken cancellationToken)
        {
            var entity = await _context.Cazadores.FirstOrDefaultAsync(cancellationToken);

            if (entity == null) 
                return NotFound();

            return new CazadorDTO_Get
            {
                Id = entity.Id,
                Nombre = entity.Nombre,
                Edad = entity.Edad,
            };
        }

        [HttpPost]
        public async Task<ActionResult<CazadorDTO_Get>> Insert(CazadorDTO_PostPut dto, CancellationToken cancellationToken)
        {
            var entity = new CazadorModel
            {
                Nombre = dto.Nombre,
                Edad = dto.Edad,
            };

            _context.Cazadores.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return new CazadorDTO_Get
            {
                Id = entity.Id,
                Nombre = entity.Nombre,
                Edad = entity.Edad,
            };
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<CazadorDTO_Get>> Update(int Id, CazadorDTO_PostPut dto, CancellationToken cancellationToken)
        {
            var entity = new CazadorModel
            {
                Id = Id,
                Nombre = dto.Nombre,
                Edad = dto.Edad,
            };

            _context.Entry(entity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                var exist = _context.Cazadores.Any(e => e.Id == Id);
                if (!exist)
                    return NotFound();
                else
                    throw;
            }

            return new CazadorDTO_Get
            {
                Id = entity.Id,
                Nombre = entity.Nombre,
                Edad = entity.Edad,
            };
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id, CancellationToken cancellationToken)
        {
            var entity = await _context.Cazadores.FindAsync(Id, cancellationToken);
            if (entity == null) 
                return NotFound();
            
            _context.Cazadores.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return NoContent();
        }
    }
}
