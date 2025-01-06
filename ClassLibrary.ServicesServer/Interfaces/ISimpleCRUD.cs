using ClassLibrary.Models.DTOs;

namespace ClassLibrary.ServicesServer.Interfaces
{
    public interface ISimpleCRUD<T>
    {
        public Task<ApiResponseDTO<IEnumerable<T>>> GetAllAsync(CancellationToken cancellationToken);
        public Task<ApiResponseDTO<T>> CreateAsync(T dto, CancellationToken cancellationToken);
        public Task<ApiResponseDTO<object>> DeleteAsync(T dto, CancellationToken cancellationToken);
    }
}
