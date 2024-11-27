# WebApi .NET 9

## Dependencias
```
ClassLibrary.Models
ClassLibrary.ServicesServer
```

### Tabla de Contenidos
- [Ir a Implementar Swagger o Scalar](#implementar-swagger-o-scalar)
- [Ir a Auth con JWT](#auth-con-jwt)
- [Ir a CodeFirst](#codefirst)
- [Ir a DataBaseFirst](#databasefirst)
- [Ir a Docker](#docker)
- [Ir a Docker SQL Server](#docker-sql-server)
- [Ir a SQL Server](#sql-server)

## Implementar Swagger o Scalar
* La aplicacion se levantara en [https://localhost:7283/openapi/v1.json](https://localhost:7283/openapi/v1.json)
### Swagger
* Dependencia
```
Swashbuckle.AspNetCore
```
* Program.cs
```
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "Swagger Api v1");
    });
}
```
* La aplicacion se levantara en [https://localhost:7283/Swagger](https://localhost:7283/Swagger)
### Scalar
* Dependencia
```
Scalar.AspNetCore
```
* Program.cs
```
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}
```
* La aplicacion se levantara en [https://localhost:7283/scalar/v1](https://localhost:7283/scalar/v1)

## Auth con JWT
[jwt.io](https://jwt.io/) <br>
[SHA256](https://tools.keycdn.com/sha256-online-generator)
* Dependencia
```
Microsoft.AspNetCore.Authentication.JwtBearer
```
* appsettings.json
```
  "JWT": {
    "ExpireMin": 60,
    "Issuer": "http://id.domain.com",
    "Audience": "http://domain.com",
    "Key": "TheKeyTheKeyTheKeyTheKeyTheKeyTheKey"
  }
```
* Metodo Creacion del Token
```
public class TokenGenerator : ITokenGenerator
{
    public TokenDTO GenerateToken(AuthDTO auth)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var claims = new List<Claim>() {
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Sub, auth.UserId),
            new(JwtRegisteredClaimNames.Email, auth.Email),
        };

        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(auth.ExpiresMin),
            Issuer = auth.Issuer,
            Audience = auth.Audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(auth.Key)), SecurityAlgorithms.HmacSha256Signature),
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return new TokenDTO()
        {
            Token = tokenHandler.WriteToken(token)
        };
    }
}
```
* Program.cs
```
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]!)),
            ValidIssuer = builder.Configuration["JWT:Issuer"]!,
            ValidAudience = builder.Configuration["JWT:Audience"]!,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            ValidateIssuer = true,
            ValidateAudience = true,
        };
    });
```
```
app.UseAuthentication();
app.UseAuthorization();
```

## CodeFirst
* Dependencia
```
Microsoft.EntityFrameworkCore.Design
Microsoft.EntityFrameworkCore.SqlServer
```
* appsettings.json
```
  "ConnectionStrings": {
    "RutaWebSQL": "Data Source=Servidor; Initial Catalog=db_testing; User ID=testing; Password=testing; TrustServerCertificate=True;",
    "RutaSQL": "Server=localhost; Database=db_testing; User ID=testing; Password=testing; TrustServerCertificate=True;"
  }
```
* Program.cs
```
builder.Services.AddDbContext<EntityDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("RutaSQL"));
});
```
* Crear la clase de conexión DbContext.cs
1. Agregar DbContexto para una conexión
```
public EntityDbContext(DbContextOptions options) : base(options)
{
}
```
2. Agregar DbContext para multiples conexiones
```
public EntityDbContext(DbContextOptions<EntityDbContext> options) : base(options)
{
}
```
* Emigrar entidades
1. Conexión única Consola NuGet
```
Add-Migration Inicial
Update-DataBase
```
2. Conexión múltiple Consola NuGet
```
Add-Migration -Context EntityDbContext Inicial
Update-DataBase -Context EntityDbContext
```

## DataBaseFirst
* Dependencia
```
Microsoft.EntityFrameworkCore.Design
Microsoft.EntityFrameworkCore.SqlServer
```
* Consola NuGet
```
Scaffold-DbContext "Server=localhost; Database=db_testing; User ID=testing; Password=testing; TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models
```

## Docker
* Docker file
* Crear la imagen
```
docker build -t webapi.testing:1.0 .
```
* Correr el contenedor
```
docker run -d -p 8080:80 --name api-microservicio webapi.testing:1.0
```

## Docker SQL Server
* Descargar imagen SQL Server
```
docker pull mcr.microsoft.com/mssql/server 
```
* Crear contenedor
* -e "ACCEPT_EULA=Y" -> aceptar términos y condiciones
* -e "MSSQL_SA_PASSWORD=mysecretpassword" -> Contraseña SA
* -e "MSSQL_PID=Developer" -> Versión developer
* -p 1433:1433 -> Puerto local:puerto contenedor
* --name SQLServer -> Nombre del contenedor
```
docker container create -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=mysecretpassword" -e "MSSQL_PID=Developer" -p 1433:1433 --name SQLServer mcr.microsoft.com/mssql/server
```
* Iniciar contenedor
```
docker container start <CONTAINER ID>
```

## SQL Server
* Revisar usuarios
```
SELECT 
	NAME AS LoginName, 
	TYPE_DESC AS AccountType, 
	create_date, 
	modify_date,
	TYPE
FROM sys.server_principals
WHERE TYPE IN ('S', 'U', 'G');
GO
```
* Crear base de datos db_testing con un usuario testing y clave testing
```
CREATE LOGIN testing WITH PASSWORD = 'testing', CHECK_POLICY = OFF;
GO
CREATE DATABASE db_testing
GO
USE db_testing
GO
CREATE USER testing FOR LOGIN testing;
GO
EXEC sp_addrolemember 'db_owner', 'testing';
GO
```