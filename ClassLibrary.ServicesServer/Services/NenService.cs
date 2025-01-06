using ClassLibrary.Models.DTOs;
using ClassLibrary.ServicesServer.Connections;
using ClassLibrary.ServicesServer.Interfaces;

namespace ClassLibrary.ServicesServer.Services
{
    public class NenService : IBaseCRUD<NenDTO, NenGetDTO>
    {
        private readonly EntityDbContext _dbContext;

        public NenService(EntityDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<ApiResponseDTO<IEnumerable<NenGetDTO>>> GetAllAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponseDTO<NenGetDTO>> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponseDTO<NenDTO>> CreateAsync(NenDTO hunterDTO, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponseDTO<NenDTO>> UpdateAsync(int id, NenDTO hunterDTO, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponseDTO<object>> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
