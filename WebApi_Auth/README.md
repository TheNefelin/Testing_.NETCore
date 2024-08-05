# WebApi Auth with Identity

### Dependency
```
Microsoft.AspNetCore.Identity.EntityFrameworkCore
Microsoft.EntityFrameworkCore.SqlServer
Microsoft.EntityFrameworkCore.Tools
Swashbuckle.AspNetCore
```

### appsettings.json
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
  }
}

```

### WebApiDbContext.cs
* Connection Class
```
public class WebApiDbContext : IdentityDbContext
{
    public WebApiDbContext(DbContextOptions options) : base(options)
    {
    }
}
```

### Program Class (Program.cs)
```
// Servicio Conexion -------------------------------------------------
builder.Services.AddDbContext<WebApiDbContext>(opcion =>
    opcion.UseSqlServer(builder.Configuration.GetConnectionString("RutaSQL"))
);
// -------------------------------------------------------------------
```

```
// Agrega Identity a los EndPonts para autenticar --------------------
builder.Services.AddIdentityApiEndpoints<IdentityUser>()
    .AddEntityFrameworkStores<WebApiDbContext>();
// -------------------------------------------------------------------
```

```
// Agrega los EndPoint de Identity -----------------------------------
app.MapIdentityApi<IdentityUser>();
// -------------------------------------------------------------------
```

### Migration
```
// crea las tablas de indetity
Add-Migration Inicial

// inicia la migracion creando las tablas de Identity en la base de datos
update-database
```
