# Entity Framework ORM

### Dependencies
```
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

### Create DBContext Class for EntityFramework
* Add Entity/Model/Class in this class
* Add SeedData if requere

> For single DBContext class
```
public class WebApiDbContext : DbContext
{
    public WebApiDbContext(DbContextOptions options) : base(options) { }
}
```
> For multiple DBContext class
```
public class WebApiDbContext : DbContext
{
    public WebApiDbContext(DbContextOptions<WebApiDbContext> options) : base(options) { }
}
```

### Add Dependency Injection in Program.cs
```
builder.Services.AddDbContext<WebApiDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("RutaSQL"));
});
```
