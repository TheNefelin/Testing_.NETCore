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
