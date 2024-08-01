# WebApi Auth JWT

### Dependency
```
Dapper
Microsoft.AspNetCore.Authentication.JwtBearer
Microsoft.EntityFrameworkCore.SqlServer
Swashbuckle.AspNetCore
BibliotecaDeClases
```

### Config File
* appsettings.Development.json
```
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "RutaWebSQL": "Data Source=Servidor; Initial Catalog=BaseDeDatos; User ID=Usuario; Password=Contraseña; TrustServerCertificate=True;",
    "RutaSQL": "Server=Servidor; Database=BaseDeDatos; User ID=Usuario; Password=Contraseña; TrustServerCertificate=True; Trusted_Connection=True;"
  },
  "JWT": {
    "Key": "TheSecretKey",
    "Issuer": "TheIssuer",
    "Audience": "TheAudience",
    "Subject": "TheSubject",
    "ExpireMin": 60
  }
}
```

### Connection Class
```
public class WebApiDbContext
{
    private readonly IDbConnection _connection;

    public WebApiDbContext(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task<AuthResponseDTO> AuthRegister(
        AuthRegisterDTO dto,
        CancellationToken cancellationToken)
    {
        var result = await _connection.QueryFirstAsync<AuthResponseDTO>(new CommandDefinition(
            $"Auth_Register @{nameof(dto.Email)}, @{nameof(dto.UserName)}, @{nameof(dto.Password)}, @{nameof(dto.PasswordConfirmed)}",
            new { dto.Email, dto.UserName, dto.Password, dto.PasswordConfirmed },
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
```

### Add SwaggerApiPadLockFilter Class
```
public class SwaggerApiPadLockFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var hasAuthorizeAttribute = context.MethodInfo.DeclaringType!
            .GetCustomAttributes(true)
            .Union(context.MethodInfo.GetCustomAttributes(true))
            .OfType<AuthorizeAttribute>().Any();

        if (hasAuthorizeAttribute)
        {
            operation.Security = new List<OpenApiSecurityRequirement>
            {
                new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                }
            };
        }
    }
}
```

### Program Class
* Dapper dependency injection
```
// Servicio Conexion -------------------------------------------------
builder.Services.AddSingleton<IDbConnection>(options =>
    new SqlConnection(builder.Configuration.GetConnectionString("RutaSQL"))
);

builder.Services.AddSingleton<WebApiDbContext>();
// -------------------------------------------------------------------
```

* JWT config
```
// Servicio JWT ------------------------------------------------------
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(jwtOptions =>
{
    jwtOptions.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateLifetime = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JWT.TheIssuer"],
        ValidAudience = builder.Configuration["JWT.TheAudience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:TheSecretKey"]!)),
    };
});
// -------------------------------------------------------------------
```

* Swagger config
```
// Modificar Swagger para que muestre los candados -------------------
builder.Services.AddSwaggerGen(options =>
{
    // Agrega el login arriba de la Api
    options.AddSecurityDefinition(
        //name: JwtBearerDefaults.AuthenticationScheme,
        name: "Bearer",
        securityScheme: new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Description = "Ingrese Token Bearer",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            BearerFormat = "JWT",
            //Scheme = "Bearer"
        }
    );

    // Agrega los candados a cada uno del CRUD
    // opcion.OperationFilter<SecurityRequirementsOperationFilter>();
    options.OperationFilter<SwaggerApiPadLockFilter>();
});
// -------------------------------------------------------------------
```

* Add authentication
```
// Usar JWT ----------------------------------------------------------
app.UseAuthentication();
// -------------------------------------------------------------------
app.UseAuthorization();
```