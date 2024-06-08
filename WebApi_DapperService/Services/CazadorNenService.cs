using BibliotecaDeClases.DTOs;
using Dapper;
using System.Data;
using System.Data.Common;
using WebApi_DapperService.Classes;

namespace WebApi_DapperService.Services
{
    public class CazadorNenService : IBaseService<CazadorNenDTO>
    {
        private readonly IDbConnection _connection;

        public CazadorNenService(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IApiActionResult<IEnumerable<CazadorNenDTO>>> GetAll(CancellationToken cancellationToken)
        {
            try
            {
                var data = await _connection.QueryAsync<CazadorNenDTO>(
                    new CommandDefinition(
                        $"CazadorNen_GetAll",
                        commandType: CommandType.StoredProcedure,
                        transaction: default,
                        cancellationToken: cancellationToken
                ));

                return new ApiActionResult<IEnumerable<CazadorNenDTO>>(200, "Ok", data);
            }
            catch (Exception ex)
            {
                return new ApiActionResult<IEnumerable<CazadorNenDTO>>(500, ex.ToString());
            }
        }

        public async Task<IApiActionResult<CazadorNenDTO>> Insert(CazadorNenDTO dto, CancellationToken cancellationToken)
        {
            try
            {
                var dbResp = await _connection.QueryFirstAsync<DbResponse>(
                    new CommandDefinition(
                        $"CazadorNen_Insert",
                        new { dto.Id_Cazador, dto.Id_Nen },
                        commandType: CommandType.StoredProcedure,
                        transaction: default,
                        cancellationToken: cancellationToken));

                // fallo en el procedimiento almacenado desde la base de datos
                if (dbResp.StatusCode != 201)
                    return new ApiActionResult<CazadorNenDTO>(dbResp.StatusCode, dbResp.Msge);

                // si todo sale OK!
                return new ApiActionResult<CazadorNenDTO>(dbResp.StatusCode, dbResp.Msge, dto);
            }
            catch (Exception ex)
            {
                return new ApiActionResult<CazadorNenDTO>(500, ex.ToString());
            }
        }

        public async Task<IApiActionResult> Delete(CazadorNenDTO dto, CancellationToken cancellationToken)
        {
            try
            {
                var dbResp = await _connection.QueryFirstAsync<DbResponse>(
                    new CommandDefinition(
                        $"CazadorNen_Delete",
                        new { dto.Id_Cazador, dto.Id_Nen },
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
