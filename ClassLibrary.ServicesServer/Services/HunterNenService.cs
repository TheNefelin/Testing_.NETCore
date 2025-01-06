using ClassLibrary.Models.DTOs;
using ClassLibrary.Models.Entities;
using ClassLibrary.ServicesServer.Connections;
using ClassLibrary.ServicesServer.Helpers;
using ClassLibrary.ServicesServer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ClassLibrary.ServicesServer.Services
{
    public class HunterNenService : ISimpleCRUD<HunterNenDTO>
    {
        private readonly EntityDbContext _dbContext;

        public HunterNenService(EntityDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponseDTO<IEnumerable<HunterNenDTO>>> GetAllAsync(CancellationToken cancellationToken)
        {
            try
            {
                var entities = await _dbContext.Hunter_Nen.ToListAsync(cancellationToken);

                if (entities == null || !entities.Any()) return ApiResponseHelper.NotFound<IEnumerable<HunterNenDTO>>();

                var dtos = entities.Select(e => new HunterNenDTO
                {
                    Hunter_Id = e.Hunter_Id,
                    Nen_Id = e.Nen_Id,
                });

                return ApiResponseHelper.Success(dtos);
            }
            catch (Exception ex)
            {
                return ApiResponseHelper.Error<IEnumerable<HunterNenDTO>>(ex.Message);
            }
        }

        public async Task<ApiResponseDTO<HunterNenDTO>> CreateAsync(HunterNenDTO dto, CancellationToken cancellationToken)
        {
            try
            {
                var exist = _dbContext.Hunter_Nen.Any(e => e.Hunter_Id == dto.Hunter_Id && e.Nen_Id == dto.Nen_Id);
                if (exist) return ApiResponseHelper.Conflict<HunterNenDTO>("The item already exists ");

                var entity = new Hunter_Nen
                {
                    Hunter_Id = dto.Hunter_Id,
                    Nen_Id = dto.Nen_Id,
                };

                _dbContext.Hunter_Nen.Add(entity);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return ApiResponseHelper.Success(dto);
            }
            catch (Exception ex)
            {
                return ApiResponseHelper.Error<HunterNenDTO>(ex.Message);
            }
        }

        public async Task<ApiResponseDTO<object>> DeleteAsync(HunterNenDTO dto, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await _dbContext.Hunter_Nen.FirstOrDefaultAsync(e => e.Hunter_Id == dto.Hunter_Id && e.Nen_Id == dto.Nen_Id);
                if (entity == null) return ApiResponseHelper.NotFound<object>();

                _dbContext.Hunter_Nen.Remove(entity);
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
