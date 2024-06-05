using BibliotecaDeClases.Classes;
using BibliotecaDeClases.DTOs;
using Dapper;
using System.Data;

namespace WebApi_DapperService.Services
{
    public class CazadorService : IBaseService<CazadorDTO_Get, CazadorDTO_PostPut>
    {
        private readonly IDbConnection _connection;

        public CazadorService(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<CazadorDTO_Get>> GetAll(CancellationToken cancellationToken)
        {
            return await _connection.QueryAsync<CazadorDTO_Get>(
                new CommandDefinition(
                    $"Cazadores_GetAll",
                    commandType: CommandType.StoredProcedure,
                    transaction: default,
                    cancellationToken: cancellationToken
            ));
        }

        public async Task<CazadorDTO_Get?> GetById(int Id, CancellationToken cancellationToken)
        {
            return await _connection.QueryFirstOrDefaultAsync<CazadorDTO_Get>(
                new CommandDefinition(
                    $"Cazadores_GetById",
                    commandType: CommandType.StoredProcedure,
                    transaction: default,
                    cancellationToken: cancellationToken
            ));
        }

        public async Task<DbResponse> Insert(CazadorDTO_PostPut dto, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<DbResponse> Update(int Id, CazadorDTO_PostPut dto, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<DbResponse> Delete(int Id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

    }
}
