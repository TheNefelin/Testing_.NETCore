# DataBase First

> Entity/Model/Class are generated from the DataBase

### Dependencies
```
Microsoft.EntityFrameworkCore.Design
Microsoft.EntityFrameworkCore.SqlServer
Swashbuckle.AspNetCore
```

### Emigrate Database table to Models (Package Manager Console)
```
Scaffold-DbContext "Server=Servidor\Instancia; Database=BaseDeDatos; User ID=Usuario; Password=Contraseña; Trusted_Connection=True; TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models
```