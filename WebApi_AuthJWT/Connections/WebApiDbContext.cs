using BibliotecaDeClases.DTOs;
using BibliotecaDeClases.Models;
using Dapper;
using System.Data;

namespace WebApi_AuthJWT.Connections
{
    public class WebApiDbContext
    {
        private readonly IDbConnection _connection;

        public WebApiDbContext(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<AuthResponseDTO> AuthRegister(
            AuthRegister authRegister,
            CancellationToken cancellationToken)
        {
            var result = await _connection.QueryFirstAsync<AuthResponseDTO>(new CommandDefinition(
                $"Auth_Register @{nameof(authRegister.Id)}, @{nameof(authRegister.Email)}, @{nameof(authRegister.Password)}, @{nameof(authRegister.PasswordConfirmed)}",
                new { authRegister.Id, authRegister.Email, authRegister.Password, authRegister.PasswordConfirmed },
                cancellationToken: cancellationToken));

            return result;
        }

        public async Task<AuthResponseDTO> AuthLogin(
            AuthLoginDTO dto,
            CancellationToken cancellationToken)
        {
            var result = await _connection.QueryFirstAsync<AuthResponseDTO>(new CommandDefinition(
                $"Auth_Login @{nameof(dto.Email)}, @{nameof(dto.Password)}",
                new { dto.Email, dto.Password },
                cancellationToken: cancellationToken));

            return result;
        }
    }
}
