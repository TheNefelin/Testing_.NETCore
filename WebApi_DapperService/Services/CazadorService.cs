﻿using BibliotecaDeClases.DTOs;
using Dapper;
using System.Data;
using WebApi_DapperService.Classes;

namespace WebApi_DapperService.Services
{
    public class CazadorService : IBaseService<CazadorDTO_Get, CazadorDTO_PostPut>
    {
        private readonly IDbConnection _connection;

        public CazadorService(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IApiActionResult<IEnumerable<CazadorDTO_Get>>> GetAll(CancellationToken cancellationToken)
        {
            try
            {
                var data = await _connection.QueryAsync<CazadorDTO_Get>(
                    new CommandDefinition(
                        $"Cazadores_GetAll",
                        commandType: CommandType.StoredProcedure,
                        transaction: default,
                        cancellationToken: cancellationToken
                ));

                return new ApiActionResult<IEnumerable<CazadorDTO_Get>>(200, "Ok", data);
            }
            catch (Exception ex) {
                return new ApiActionResult<IEnumerable<CazadorDTO_Get>>(500, ex.ToString());
            }
        }

        public async Task<IApiActionResult<CazadorDTO_Get>> GetById(int Id, CancellationToken cancellationToken)
        {
            try
            {
                var data = await _connection.QueryFirstOrDefaultAsync<CazadorDTO_Get>(
                    new CommandDefinition(
                        $"Cazadores_GetById",
                        new { Id },
                        commandType: CommandType.StoredProcedure,
                        transaction: default,
                        cancellationToken: cancellationToken));

                if (data == null)
                    return new ApiActionResult<CazadorDTO_Get>(400, "El Id No Existe");

                return new ApiActionResult<CazadorDTO_Get>(200, "Ok", data);
            }
            catch (Exception ex)
            {
                return new ApiActionResult<CazadorDTO_Get>(500, ex.ToString());
            }
        }

        public async Task<IApiActionResult<CazadorDTO_Get>> Insert(CazadorDTO_PostPut dto, CancellationToken cancellationToken)
        {
            try
            {
                var dbResp = await _connection.QueryFirstAsync<DbResponse>(
                    new CommandDefinition(
                        $"Cazadores_Insert",
                        new { dto.Nombre, dto.Edad },
                        commandType: CommandType.StoredProcedure,
                        transaction: default,
                        cancellationToken: cancellationToken));

                // fallo en el procedimiento almacenado desde la base de datos
                if (dbResp.StatusCode != 201)
                    return new ApiActionResult<CazadorDTO_Get>(dbResp.StatusCode, dbResp.Msge);

                // recupera en nuevo elemento
                var getById = await GetById(dbResp.Id, cancellationToken);

                // si falla la recuperacion del elemento
                if (getById.StatusCode != 200)
                    return new ApiActionResult<CazadorDTO_Get>(getById.StatusCode, getById.Message, getById.Data);

                // si todo sale OK!
                return new ApiActionResult<CazadorDTO_Get>(dbResp.StatusCode, dbResp.Msge, getById.Data);
            }
            catch (Exception ex) {
                return new ApiActionResult<CazadorDTO_Get>(500, ex.ToString());
            }
        }

        public async Task<IApiActionResult<CazadorDTO_Get>> Update(int Id, CazadorDTO_PostPut dto, CancellationToken cancellationToken)
        {
            try
            {
                var dbResp = await _connection.QueryFirstAsync<DbResponse>(
                    new CommandDefinition(
                        $"Cazadores_Update",
                        new { Id, dto.Nombre, dto.Edad },
                        commandType: CommandType.StoredProcedure,
                        transaction: default,
                        cancellationToken: cancellationToken));

                // fallo en el procedimiento almacenado desde la base de datos
                if (dbResp.StatusCode != 202)
                    return new ApiActionResult<CazadorDTO_Get>(dbResp.StatusCode, dbResp.Msge);

                // recupera el elemento modificado
                var getById = await GetById(dbResp.Id, cancellationToken);

                // si falla la recuperacion del elemento
                if (getById.StatusCode != 200)
                    return new ApiActionResult<CazadorDTO_Get>(getById.StatusCode, getById.Message, getById.Data);

                // si todo sale OK!
                return new ApiActionResult<CazadorDTO_Get>(dbResp.StatusCode, dbResp.Msge, getById.Data);
            }
            catch (Exception ex)
            {
                return new ApiActionResult<CazadorDTO_Get>(500, ex.ToString());
            }
        }

        public async Task<IApiActionResult> Delete(int Id, CancellationToken cancellationToken)
        {
            try
            {
                var dbResp = await _connection.QueryFirstAsync<DbResponse>(
                    new CommandDefinition(
                        $"Cazadores_Delete",
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
