# Console App for Testing

## Dependencies
```
Microsoft.AspNetCore.Cryptography.KeyDerivation
Microsoft.EntityFrameworkCore
Microsoft.EntityFrameworkCore.Design
Microsoft.EntityFrameworkCore.SqlServer
Microsoft.EntityFrameworkCore.Tools
```

## Migrations
```
Add-Migration init
update-database
```

## Auth and Encrypt
> [JWT.io](https://jwt.io/) <br>
> [Generate SHA256 Key](https://tools.keycdn.com/sha256-online-generator)

* DbContextSingleton: Singleton instance for connection 
* AppDbContextFactory: For migrations
* AppDbContext: To use ORM that interacts with the database.

### Encryption
* PlainText: Text to Encrypt
* Pass: Password as KEY for encrypt
* IV: Random Byte array as salt for Encrypt en Decrypt 
* EncryptedText: Text for Decrypt
```
internal class Encryption
{
    public static (string Encrypted, string IV) Encrypt(string plainText, string pass)
    {
        using var aes = Aes.Create();
        aes.Key = GetAesKey(pass);
        aes.IV = RandomNumberGenerator.GetBytes(16);

        ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

        using var ms = new MemoryStream();
        using var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
        using (var sw = new StreamWriter(cs))
        {
            sw.Write(plainText);
        }

        byte[] encrypted = ms.ToArray();

        return (Convert.ToBase64String(encrypted), Convert.ToBase64String(aes.IV));
    }

    public static string Decrypt(string encryptedText, string pass, string iv)
    {
        byte[] cipherBytes = Convert.FromBase64String(encryptedText);

        using var aes = Aes.Create();
        aes.Key = GetAesKey(pass);
        aes.IV = Convert.FromBase64String(iv);

        ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

        using var ms = new MemoryStream(cipherBytes);
        using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
        using var sr = new StreamReader(cs);

        return sr.ReadToEnd();
    }

    private static byte[] GetAesKey(string key)
    {
        byte[] keyBytes = Encoding.UTF8.GetBytes(key);

        while (keyBytes.Length < 32)
            keyBytes = keyBytes.Concat(keyBytes).ToArray(); // Concatenamos la clave
         
        return keyBytes.Take(32).ToArray();
    }
}
```

### Password
```
internal class Password
{
    public static (string Hash, string Salt) HashPassword(string password)
    {
        byte[] saltBytes = RandomNumberGenerator.GetBytes(16); // 16 Bytes

        string hashed = HashMachie(password, saltBytes);

        return (hashed, Convert.ToBase64String(saltBytes));
    }

    public static bool VerifyPassword(string password, string hashedPassword, string salt)
    {
        byte[] saltBytes = Convert.FromBase64String(salt);

        string hashed = HashMachie(password, saltBytes);

        return hashed == hashedPassword;
    }

    private static string HashMachie(string password, byte[] saltBytes)
    {
        return Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: saltBytes,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 32)); // 32 Bytes or 64 Bytes
    }
}
```

## DataBase

### DbContextSingleton
```
internal class DbContextSingleton
{
    // El campo 'Lazy' garantiza que la instancia se cree solo una vez, de forma segura para múltiples hilos.
    private static readonly Lazy<AppDbContext> _instance = new Lazy<AppDbContext>(CreateDbContext);

    private DbContextSingleton() { }

    // Proporciona la única instancia de AppDbContext.
    public static AppDbContext Instance => _instance.Value;

    // Método para crear la instancia de AppDbContext, solo se llama la primera vez.
    private static AppDbContext CreateDbContext()
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseSqlServer("Server=localhost; Database=db_testing; User ID=testing; Password=testing; TrustServerCertificate=True;");

        return new AppDbContext(optionsBuilder.Options);
    }
}
```

### AppDbContextFactory
```
internal class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        return DbContextSingleton.Instance;
    }
}
```

### AppDbContext
```
internal class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<UserEntity> Users { get; set; }
    public DbSet<SecretEntity> Secrets { get; set; }
}
```

