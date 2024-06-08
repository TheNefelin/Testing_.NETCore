using BibliotecaDeClases.DTOs;
using Dapper;
using System.Data;
using WebApi_DapperService.Classes;

namespace WebApi_DapperService.Services
{
    public class NenService : IBaseService<NenDTO_Get, NenDTO_PostPut>
    {
        private readonly IDbConnection _connection;

        public NenService(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IApiActionResult<IEnumerable<NenDTO_Get>>> GetAll(CancellationToken cancellationToken)
        {
            try
            {
                var data = await _connection.QueryAsync<NenDTO_Get>(
                    new CommandDefinition(
                        $"Nen_GetAll",
                        commandType: CommandType.StoredProcedure,
                        transaction: default,
                        cancellationToken: cancellationToken
                ));

                return new ApiActionResult<IEnumerable<NenDTO_Get>>(200, "Ok", data);
            }
            catch (Exception ex)
            {
                return new ApiActionResult<IEnumerable<NenDTO_Get>>(500, ex.ToString());
            }
        }

        public async Task<IApiActionResult<NenDTO_Get>> GetById(int Id, CancellationToken cancellationToken)
        {
            try
            {
                var data = await _connection.QueryFirstOrDefaultAsync<NenDTO_Get>(
                    new CommandDefinition(
                        $"Nen_GetById",
                        new { Id },
                        commandType: CommandType.StoredProcedure,
                        transaction: default,
                        cancellationToken: cancellationToken));

                if (data == null)
                    return new ApiActionResult<NenDTO_Get>(400, "El Id No Existe");

                return new ApiActionResult<NenDTO_Get>(200, "Ok", data);
            }
            catch (Exception ex)
            {
                return new ApiActionResult<NenDTO_Get>(500, ex.ToString());
            }
        }

        public async Task<IApiActionResult<NenDTO_Get>> Insert(NenDTO_PostPut dto, CancellationToken cancellationToken)
        {
            try
            {
                var dbResp = await _connection.QueryFirstAsync<DbResponse>(
                    new CommandDefinition(
                        $"Nen_Insert",
                        new { dto.Nombre, dto.Descripcion },
                        commandType: CommandType.StoredProcedure,
                        transaction: default,
                        cancellationToken: cancellationToken));

                // fallo en el procedimiento almacenado desde la base de datos
                if (dbResp.StatusCode != 201)
                    return new ApiActionResult<NenDTO_Get>(dbResp.StatusCode, dbResp.Msge);

                // recupera en nuevo elemento
                var getById = await GetById(dbResp.Id, cancellationToken);

                // si falla la recuperacion del elemento
                if (getById.StatusCode != 200)
                    return new ApiActionResult<NenDTO_Get>(getById.StatusCode, getById.Message, getById.Data);

                // si todo sale OK!
                return new ApiActionResult<NenDTO_Get>(dbResp.StatusCode, dbResp.Msge, getById.Data);
            }
            catch (Exception ex)
            {
                return new ApiActionResult<NenDTO_Get>(500, ex.ToString());
            }
        }

        public async Task<IApiActionResult<NenDTO_Get>> Update(int Id, NenDTO_PostPut dto, CancellationToken cancellationToken)
        {
            try
            {
                var dbResp = await _connection.QueryFirstAsync<DbResponse>(
                    new CommandDefinition(
                        $"Nen_Update",
                        new { Id, dto.Nombre, dto.Descripcion },
                        commandType: CommandType.StoredProcedure,
                        transaction: default,
                        cancellationToken: cancellationToken));

                // fallo en el procedimiento almacenado desde la base de datos
                if (dbResp.StatusCode != 202)
                    return new ApiActionResult<NenDTO_Get>(dbResp.StatusCode, dbResp.Msge);

                // recupera el elemento modificado
                var getById = await GetById(dbResp.Id, cancellationToken);

                // si falla la recuperacion del elemento
                if (getById.StatusCode != 200)
                    return new ApiActionResult<NenDTO_Get>(getById.StatusCode, getById.Message, getById.Data);

                // si todo sale OK!
                return new ApiActionResult<NenDTO_Get>(dbResp.StatusCode, dbResp.Msge, getById.Data);
            }
            catch (Exception ex)
            {
                return new ApiActionResult<NenDTO_Get>(500, ex.ToString());
            }
        }

        public async Task<IApiActionResult> Delete(int Id, CancellationToken cancellationToken)
        {
            try
            {
                var dbResp = await _connection.QueryFirstAsync<DbResponse>(
                    new CommandDefinition(
                        $"Nen_Delete",
                        new { Id },
                        commandType: CommandType.StoredProcedure,
                        transaction: default,
                        cancellationToken: cancellationToken));

                return new ApiActionResult(dbResp.StatusCode, dbResp.Msge);
            }
            catch (Exception ex)
            {
                return new ApiActionResult(500, ex.ToString());
            }
        }
    }
}
