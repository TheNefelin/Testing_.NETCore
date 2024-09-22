using Microsoft.EntityFrameworkCore.Design;

namespace ConsoleAppTesting.Contexts
{
    internal class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            return DbContextSingleton.Instance;
        }
    }
}
