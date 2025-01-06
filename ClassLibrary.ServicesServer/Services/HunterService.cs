using ClassLibrary.Models.DTOs;
using ClassLibrary.Models.Entities;
using ClassLibrary.ServicesServer.Connections;
using ClassLibrary.ServicesServer.Helpers;
using ClassLibrary.ServicesServer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ClassLibrary.ServicesServer.Services
{
    public class HunterService : IBaseCRUD<HunterDTO, HunterGetDTO>
    {
        private readonly EntityDbContext _dbContext;

        public HunterService(EntityDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponseDTO<IEnumerable<HunterGetDTO>>> GetAllAsync(CancellationToken cancellationToken)
        {
            try
            {
                var result = await _dbContext.Hunter
                    .Include(e => e.Hunter_Nen)
                    .ThenInclude(e => e.Nen)
                    .ToListAsync(cancellationToken);

                if (result == null || !result.Any())
                {
                    return ApiResponseHelper.NotFound<IEnumerable<HunterGetDTO>>();
                }

                var data = result.Select(e => new HunterGetDTO
                {
                    Id = e.Id,
                    Name = e.Name,
                    Age = e.Age,
                    Nen = e.Hunter_Nen.Select(n => new NenDTO
                    {
                        Id = n.Nen.Id,
                        Name = n.Nen.Name,
                        Description = n.Nen.Description,
                    }).ToList(),
                });

                return ApiResponseHelper.Success(data);
            }
            catch (Exception ex)
            {
                return ApiResponseHelper.Error<IEnumerable<HunterGetDTO>>(ex.Message);
            }
        }

        public async Task<ApiResponseDTO<HunterGetDTO>> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _dbContext.Hunter
                    .Include(e => e.Hunter_Nen)
                    .ThenInclude(e => e.Nen)
                    .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

                if (result == null)
                {
                    return ApiResponseHelper.NotFound<HunterGetDTO>();
                }

                var data = new HunterGetDTO()
                {
                    Id = result.Id,
                    Name = result.Name,
                    Age = result.Age,
                    Nen = result.Hunter_Nen.Select(n => new NenDTO
                    {
                        Id = n.Nen.Id,
                        Name = n.Nen.Name,
                        Description = n.Nen.Description,
                    }).ToList(),
                };

                return  ApiResponseHelper.Success(data);
            }
            catch (Exception ex)
            {
                return ApiResponseHelper.Error<HunterGetDTO>(ex.Message);
            }
        }

        public async Task<ApiResponseDTO<HunterDTO>> CreateAsync(HunterDTO hunterDTO, CancellationToken cancellationToken)
        {
            try
            {
                if (hunterDTO.Age <= 0)
                {
                    return ApiResponseHelper.BadRequest<HunterDTO>("Age must be a positive number.");
                }

                var hunter = new Hunter
                {
                    Id = 0,
                    Name = hunterDTO.Name,
                    Age = hunterDTO.Age,
                };

                _dbContext.Hunter.Add(hunter);
                await _dbContext.SaveChangesAsync(cancellationToken);

                hunterDTO.Id = hunter.Id;

                return ApiResponseHelper.Success(hunterDTO);
            }
            catch (Exception ex)
            {
                return ApiResponseHelper.Error<HunterDTO>(ex.Message);
            }       
        }

        public async Task<ApiResponseDTO<HunterDTO>> UpdateAsync(int id, HunterDTO hunterDTO, CancellationToken cancellationToken)
        {
            try
            {
                if (id != hunterDTO.Id)
                {
                    return ApiResponseHelper.BadRequest<HunterDTO>("Id doesn't match.");
                }

                if (hunterDTO.Age <= 0)
                {
                    return ApiResponseHelper.BadRequest<HunterDTO>("Age must be a positive number.");
                }

                var existingHunter = await _dbContext.Hunter.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
                if (existingHunter == null)
                {
                    return ApiResponseHelper.NotFound<HunterDTO>();
                }

                existingHunter.Name = hunterDTO.Name;
                existingHunter.Age = hunterDTO.Age;

                await _dbContext.SaveChangesAsync(cancellationToken);

                return ApiResponseHelper.Success(hunterDTO);
            }
            catch (Exception ex)
            {
                return ApiResponseHelper.Error<HunterDTO>(ex.Message);
            }
        }

        public async Task<ApiResponseDTO<object>> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            try
            {
                var hunter = await _dbContext.Hunter.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
                if (hunter == null)
                {
                    return ApiResponseHelper.NotFound<object>();
                }

                _dbContext.Hunter.Remove(hunter);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return ApiResponseHelper.Success<object>();
            }
            catch (Exception ex)
            {
                return ApiResponseHelper.Error<object>(ex.Message);
            }
        }
    }
}
