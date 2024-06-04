using BibliotecaDeClases.DTOs;
using BibliotecaDeClases.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi_EF_orm.Connections;

namespace WebApi_EF_orm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NenController : ControllerBase
    {
        private readonly WebApiDbContext _context;

        public NenController(WebApiDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<NenDTO_Get>>> GetAll(CancellationToken cancellationToken)
        {
            var entities = await _context.Nen.ToListAsync(cancellationToken);

            return entities.Select(entity => new NenDTO_Get
            {
                Id = entity.Id,
                Nombre = entity.Nombre,
                Descripcion = entity.Descripcion,
            }).ToList();
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<NenDTO_Get>> GetById(int Id, CancellationToken cancellationToken)
        {
            var entity = await _context.Nen.FirstOrDefaultAsync(cancellationToken);

            if (entity == null)
                return NotFound();

            return new NenDTO_Get
            {
                Id = entity.Id,
                Nombre = entity.Nombre,
                Descripcion = entity.Descripcion,
            };
        }

        [HttpPost]
        public async Task<ActionResult<NenDTO_Get>> Insert(NenDTO_PostPut dto, CancellationToken cancellationToken)
        {
            var entity = new NenModel
            {
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
            };

            _context.Nen.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return new NenDTO_Get
            {
                Id = entity.Id,
                Nombre = entity.Nombre,
                Descripcion = entity.Descripcion,
            };
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<NenDTO_Get>> Update(int Id, NenDTO_PostPut dto, CancellationToken cancellationToken)
        {
            var entity = new NenModel
            {
                Id = Id,
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
            };

            _context.Entry(entity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                var exist = _context.Nen.Any(e => e.Id == Id);
                if (!exist)
                    return NotFound();
                else
                    throw;
            }

            return new NenDTO_Get
            {
                Id = entity.Id,
                Nombre = entity.Nombre,
                Descripcion = entity.Descripcion,
            };
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id, CancellationToken cancellationToken)
        {
            var entity = await _context.Nen.FindAsync(Id, cancellationToken);
            if (entity == null)
                return NotFound();

            _context.Nen.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return NoContent();
        }
    }
}
