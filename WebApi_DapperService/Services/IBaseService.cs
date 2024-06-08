using WebApi_DapperService.Classes;

namespace WebApi_DapperService.Services
{
    // T_dtoGet y T_dtoPostPut son clases genericas <T> que corresponden a los dtos.
    public interface IBaseService<T_dtoGet, T_dtoPostPut>
    {
        public Task<IApiActionResult<IEnumerable<T_dtoGet>>> GetAll(CancellationToken cancellationToken);
        public Task<IApiActionResult<T_dtoGet>> GetById(int Id, CancellationToken cancellationToken);
        public Task<IApiActionResult<T_dtoGet>> Insert(T_dtoPostPut dto, CancellationToken cancellationToken);
        public Task<IApiActionResult<T_dtoGet>> Update(int Id, T_dtoPostPut dto, CancellationToken cancellationToken);
        public Task<IApiActionResult> Delete(int Id, CancellationToken cancellationToken);
    }

    public interface IBaseService<T>
    {
        public Task<IApiActionResult<IEnumerable<T>>> GetAll(CancellationToken cancellationToken);
        public Task<IApiActionResult<T>> Insert(T dto, CancellationToken cancellationToken);
        public Task<IApiActionResult> Delete(T dto, CancellationToken cancellationToken);
    }
}
