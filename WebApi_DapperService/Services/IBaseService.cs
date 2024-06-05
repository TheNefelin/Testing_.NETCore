using Azure;
using BibliotecaDeClases.Classes;
using System.Net;

namespace WebApi_DapperService.Services
{
    // T_dtoGet y T_dtoPostPut son clases genericas <T> que corresponden a los dtos.
    public interface IBaseService<T_dtoGet, T_dtoPostPut>
    {
        public Task<IEnumerable<T_dtoGet>> GetAll(CancellationToken cancellationToken);
        public Task<T_dtoGet?> GetById(int Id, CancellationToken cancellationToken);
        public Task<DbResponse> Insert(T_dtoPostPut dto, CancellationToken cancellationToken);
        public Task<DbResponse> Update(int Id, T_dtoPostPut dto, CancellationToken cancellationToken);
        public Task<DbResponse> Delete(int Id, CancellationToken cancellationToken);
    }
}
