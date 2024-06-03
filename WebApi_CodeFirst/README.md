# Code First

> The DB is generated from the code

### Dependencies
```
Microsoft.EntityFrameworkCore.SqlServer
Microsoft.EntityFrameworkCore.Tools
Swashbuckle.AspNetCore
```

### Add DB Connections in Program.cs
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

### Emigrate Entities to Database (Package Manager Console)
* If single DBContext class for connection
```
Add-Migration Inicial
Update-DataBase
```
* If multiple DBContext class for connection
```
Add-Migration -Context WebApiDbContext Inicial
Update-DataBase -Context WebApiDbContext
```
