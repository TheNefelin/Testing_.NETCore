using ConsoleAppTesting.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConsoleAppTesting.Contexts
{
    internal class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<SecretEntity> Secrets { get; set; }
    }
}
