using ClassLibrary.Models.DTOs;

namespace ClassLibrary.ServicesServer.Interfaces
{
    public interface IBaseCRUD<T,TGet>
    {
        public Task<ApiResponseDTO<IEnumerable<TGet>>> GetAllAsync(CancellationToken cancellationToken);
        public Task<ApiResponseDTO<TGet>> GetByIdAsync(int id, CancellationToken cancellationToken);
        public Task<ApiResponseDTO<T>> CreateAsync(T dto, CancellationToken cancellationToken);
        public Task<ApiResponseDTO<T>> UpdateAsync(int id, T dto, CancellationToken cancellationToken);
        public Task<ApiResponseDTO<object>> DeleteAsync(int id, CancellationToken cancellationToken);
    }
}
