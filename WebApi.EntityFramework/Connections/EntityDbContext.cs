using ClassLibrary.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace WebApi.EntityFramework.Connections
{
    public class EntityDbContext : DbContext
    {
        public EntityDbContext(DbContextOptions<EntityDbContext> options) : base(options)
        {
        }

        public DbSet<Hunter> Hunter { get; set; }
        public DbSet<Nen> Nen { get; set; }
        public DbSet<Hunter_Nen> Hunter_Nen { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Hunter>(t =>
            {
                t.HasKey(c => c.Id);
                t.Property(c => c.Name).HasColumnType("VARCHAR(50)");
            });

            modelBuilder.Entity<Nen>(t => {
                t.HasKey(c => c.Id);
                t.Property(c => c.Name).HasColumnType("VARCHAR(50)");
                t.Property(c => c.Description).HasColumnType("VARCHAR(256)");
            });

            modelBuilder.Entity<Hunter_Nen>(t => {
                t.HasKey(c => new { c.Hunter_Id, c.Nen_Id });
                t.HasOne(c => c.Hunter).WithMany(c => c.Hunter_Nen).HasForeignKey(c => c.Hunter_Id).OnDelete(DeleteBehavior.Restrict);
                t.HasOne(c => c.Nen).WithMany(c => c.Hunter_Nen).HasForeignKey(c => c.Nen_Id).OnDelete(DeleteBehavior.Restrict);
            });

            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Hunter>().HasData(
                new Hunter { Id = 1, Name = "Gon Freecss", Age = 12 },
                new Hunter { Id = 2, Name = "Killua Zoldyck", Age = 12 },
                new Hunter { Id = 3, Name = "Kurapika Kurta", Age = 17 },
                new Hunter { Id = 4, Name = "Leorio Paladiknight", Age = 19 }
            );

            modelBuilder.Entity<Nen>().HasData(
                new Nen { Id = 1, Name = "Intensificación", Description = "Si un estudiante aumenta la cantidad de agua en el vaso durante su prueba del agua, es de Intensificación" },
                new Nen { Id = 2, Name = "Transformación", Description = "Si un estudiante cambia el sabor del agua durante su prueba del agua es un Transformador" },
                new Nen { Id = 3, Name = "Materialización", Description = "Si un estudiante hace aparecer impurezas en el agua del vaso durante su prueba ellos son Materialización" },
                new Nen { Id = 4, Name = "Emisión", Description = "Si un estudiante cambia el color del agua en el vaso durante su prueba del agua, es un Emisor" },
                new Nen { Id = 5, Name = "Manipulación", Description = "Si un estudiante mueve la hoja flotando en el agua del vaso durante su prueba del agua, es un Manipulador" },
                new Nen { Id = 6, Name = "Especialización", Description = "Si un estudiante hace algún otro efecto durante su prueba del agua, son Especialistas" }
            );

            modelBuilder.Entity<Hunter_Nen>().HasData(
                new Hunter_Nen { Hunter_Id = 1, Nen_Id = 1 },
                new Hunter_Nen { Hunter_Id = 2, Nen_Id = 2 },
                new Hunter_Nen { Hunter_Id = 3, Nen_Id = 3 },
                new Hunter_Nen { Hunter_Id = 3, Nen_Id = 6 },
                new Hunter_Nen { Hunter_Id = 4, Nen_Id = 4 }
            );
        }
    }
}
