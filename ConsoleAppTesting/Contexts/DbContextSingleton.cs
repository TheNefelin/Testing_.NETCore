using Microsoft.EntityFrameworkCore;

namespace ConsoleAppTesting.Contexts
{
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
}
