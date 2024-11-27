using ClassLibrary.Models.DTOs;
using ClassLibrary.Models.Entities;
using ClassLibrary.ServicesServer.Connections;
using ClassLibrary.ServicesServer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ClassLibrary.ServicesServer.Services
{
    public class HunterService : IServiceBaseCRUD<HunterDTO, Hunter>
    {
        private readonly EntityDbContext _dbContext;

        public HunterService(EntityDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<HunterDTO>> GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await _dbContext.Hunter.ToListAsync(cancellationToken);

            return result.Select(e => new HunterDTO
            {
                Id = e.Id,
                Name = e.Name,
                Age = e.Age,
            });
        }

        public async Task<HunterDTO?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var result = await _dbContext.Hunter.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (result == null) return null;

            return new HunterDTO()
            {
                Id = result.Id,
                Name = result.Name,
                Age = result.Age,
            };
        }

        public async Task<HunterDTO> CreateAsync(Hunter hunter, CancellationToken cancellationToken)
        {
            _dbContext.Hunter.Add(hunter);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new HunterDTO()
            {
                Id = hunter.Id,
                Name = hunter.Name,
                Age = hunter.Age,
            };
        }

        public async Task<HunterDTO?> UpdateAsync(int id, Hunter hunter, CancellationToken cancellationToken)
        {
            var existingHunter = await _dbContext.Hunter.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (existingHunter == null)
            {
                return null;
            }

            existingHunter.Name = hunter.Name;
            existingHunter.Age = hunter.Age;
           
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new HunterDTO()
            {
                Id = existingHunter.Id,
                Name = existingHunter.Name,
                Age = existingHunter.Age,
            };
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var hunter = await _dbContext.Hunter.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (hunter == null)
            {
                return false;
            }

            _dbContext.Hunter.Remove(hunter); 
            await _dbContext.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
