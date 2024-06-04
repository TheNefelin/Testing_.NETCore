using BibliotecaDeClases.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApi_EF_orm.Connections
{
    public class WebApiDbContext : DbContext
    {
        public WebApiDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<CazadorModel> Cazadores { get; set; }
        public DbSet<NenModel> Nen { get; set; }
        public DbSet<CazadorNenModel> CazadorNen { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CazadorNenModel>(t =>
            {
                t.HasKey(c => new { c.Id_Cazador, c.Id_Nen });
                t.HasOne(c => c.Cazador).WithMany(c => c.Cazador_Nen).HasForeignKey(c => c.Id_Cazador).OnDelete(DeleteBehavior.Restrict);
                t.HasOne(c => c.Nen).WithMany(c => c.Cazador_Nen).HasForeignKey(c => c.Id_Nen).OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
