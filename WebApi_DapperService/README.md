# Dapper Service

### Dependencies
```
Dapper
Microsoft.EntityFrameworkCore.SqlServer
Swashbuckle.AspNetCore
```

### Add DB Connections in appsettings.json
```
"ConnectionStrings": {
    "RutaWebSQL": "Data Source=Servidor; Initial Catalog=BaseDeDatos; User ID=Usuario; Password=Contraseña; TrustServerCertificate=True;",
    "RutaSQL": "Server=Servidor; Database=BaseDeDatos; User ID=Usuario; Password=Contraseña; TrustServerCertificate=True; Trusted_Connection=True;"
},
```

### Add conection instance and Services in Program.cs
```
builder.Services.AddTransient<IDbConnection>(options =>
    new SqlConnection(builder.Configuration.GetConnectionString("RutaSQL"))
);

builder.Services.AddTransient<IBaseService<CazadorDTO_Get, CazadorDTO_PostPut>, CazadorService>();
builder.Services.AddTransient<IBaseService<NenDTO_Get, NenDTO_PostPut>, NenService>();
builder.Services.AddTransient<IBaseService<CazadorNenDTO>, CazadorNenService>();

```
