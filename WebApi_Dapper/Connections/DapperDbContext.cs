using BibliotecaDeClases.DTOs;
using Dapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Data;

namespace WebApi_Dapper.Connections
{
    public class DapperDbContext
    {
        private readonly IDbConnection _dapper;

        public DapperDbContext(IDbConnection dapper)
        {
            _dapper = dapper;
        }

        public async Task<IEnumerable<CazadorDTO_Get>> Cazadores_GetAll(CancellationToken cancellationToken)
        {
            var result = await _dapper.QueryAsync<CazadorDTO_Get>(new CommandDefinition(
                $"Cazadores_GetAll",
                commandType: CommandType.StoredProcedure,
                transaction: default,
                cancellationToken: cancellationToken));

            return result;
        }

        public async Task<CazadorDTO_Get?> Cazadores_GetById(int Id, CancellationToken cancellationToken)
        {
            var result = await _dapper.QueryFirstOrDefaultAsync<CazadorDTO_Get>(new CommandDefinition(
                $"Cazadores_GetById",
                new { Id },
                commandType: CommandType.StoredProcedure,
                transaction: default,
                cancellationToken: cancellationToken));

            return result;
        }

        public async Task<int> Cazadores_Insert(CazadorDTO_PostPut dto, CancellationToken cancellationToken)
        {
            var result = await _dapper.QueryFirstOrDefaultAsync<int>(new CommandDefinition(
                $"Cazadores_Insert",
                new { dto.Nombre, dto.Edad },
                commandType: CommandType.StoredProcedure,
                transaction: default,
                cancellationToken: cancellationToken));
 
            return result;
        }

        public async Task Cazadores_Update(int Id, CazadorDTO_PostPut dto, CancellationToken cancellationToken)
        {
            await _dapper.QueryAsync(new CommandDefinition(
                $"Cazadores_Update",
                new { Id, dto.Nombre, dto.Edad },
                commandType: CommandType.StoredProcedure,
                transaction: default,
                cancellationToken: cancellationToken));
        }

        public async Task Cazadores_Delete(int Id, CancellationToken cancellationToken)
        {
            await _dapper.QueryAsync(new CommandDefinition(
                $"Cazadores_Delete",
                new { Id },
                commandType: CommandType.StoredProcedure,
                transaction: default,
                cancellationToken: cancellationToken));
        }

        public async Task<IEnumerable<NenDTO_Get>> Nen_GetAll(CancellationToken cancellationToken)
        {
            var result = await _dapper.QueryAsync<NenDTO_Get>(new CommandDefinition(
                $"Nen_GetAll",
                commandType: CommandType.StoredProcedure,
                transaction: default,
                cancellationToken: cancellationToken));

            return result;
        }

        public async Task<NenDTO_Get?> Nen_GetById(int Id, CancellationToken cancellationToken)
        {
            var result = await _dapper.QueryFirstOrDefaultAsync<NenDTO_Get>(new CommandDefinition(
                $"Nen_GetById",
                new { Id },
                commandType: CommandType.StoredProcedure,
                transaction: default,
                cancellationToken: cancellationToken));

            return result;
        }

        public async Task<int> Nen_Insert(NenDTO_PostPut dto, CancellationToken cancellationToken)
        {
            var result = await _dapper.QueryFirstOrDefaultAsync<int>(new CommandDefinition(
                $"Nen_Insert",
                new { dto.Nombre, dto.Descripcion },
                commandType: CommandType.StoredProcedure,
                transaction: default,
                cancellationToken: cancellationToken));

            return result;
        }

        public async Task Nen_Update(int Id, NenDTO_PostPut dto, CancellationToken cancellationToken)
        {
            await _dapper.QueryAsync(new CommandDefinition(
                $"Nen_Update",
                new { Id, dto.Nombre, dto.Descripcion },
                commandType: CommandType.StoredProcedure,
                transaction: default,
                cancellationToken: cancellationToken));
        }

        public async Task Nen_Delete(int Id, CancellationToken cancellationToken)
        {
            await _dapper.QueryAsync(new CommandDefinition(
                $"Nen_Delete",
                new { Id },
                commandType: CommandType.StoredProcedure,
                transaction: default,
                cancellationToken: cancellationToken));
        }

        public async Task<IEnumerable<CazadorNenDTO>> CazadorNen_GetAll(CancellationToken cancellationToken)
        {
            var result = await _dapper.QueryAsync<CazadorNenDTO>(new CommandDefinition(
                $"CazadorNen_GetAll",
                commandType: CommandType.StoredProcedure,
                transaction: default,
                cancellationToken: cancellationToken));

            return result;
        }

        public async Task CazadorNen_Insert(CazadorNenDTO dto, CancellationToken cancellationToken)
        {
            await _dapper.QueryFirstOrDefaultAsync<int>(new CommandDefinition(
                $"CazadorNen_Insert",
                new { dto.Id_Cazador, dto.Id_Nen },
                commandType: CommandType.StoredProcedure,
                transaction: default,
                cancellationToken: cancellationToken));
        }

        public async Task CazadorNen_Delete(CazadorNenDTO dto, CancellationToken cancellationToken)
        {
            await _dapper.QueryAsync(new CommandDefinition(
                $"CazadorNen_Delete",
                new { dto.Id_Cazador, dto.Id_Nen },
                commandType: CommandType.StoredProcedure,
                transaction: default,
                cancellationToken: cancellationToken));
        }
    }
}
