# WebApi .NET 9

## Dependencias
```
ClassLibrary.Models
ClassLibrary.ServicesServer
```

### Tabla de Contenidos
- [Ir a Implementar Swagger o Scalar](#implementar-swagger-o-scalar)
- [Ir a Auth con JWT](auth-con-jwt)
- [Ir a CodeFirst](#codefirst)
- [Ir a DataBaseFirst](#databasefirst)

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
Microsoft.EntityFrameworkCore.SqlServer
Microsoft.EntityFrameworkCore.Tools
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
* Crear la clase de conexion DbContext.cs
1. Agregar DbContext para una conexion
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
1. Conexion unica Consola NuGet
```
Add-Migration Inicial
Update-DataBase
```
2. Conexion multiple Consola NuGet
```
Add-Migration -Context EntityDbContext Inicial
Update-DataBase -Context EntityDbContext
```

## DataBaseFirst
* Dependencia
```
Microsoft.EntityFrameworkCore.SqlServer
Microsoft.EntityFrameworkCore.Tools
```
* Consola NuGet
```
Scaffold-DbContext "Server=localhost; Database=db_testing; User ID=testing; Password=testing; TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models
```
