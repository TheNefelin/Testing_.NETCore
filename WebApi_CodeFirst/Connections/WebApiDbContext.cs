using BibliotecaDeClases.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApi_CodeFirst.Connections
{
    public class WebApiDbContext : DbContext
    {
        public WebApiDbContext(DbContextOptions<WebApiDbContext> options) : base(options)
        {
        }

        public DbSet<CazadorModel> Cazadores { get; set; }
        public DbSet<NenModel> Nen { get; set; }
        public DbSet<CazadorNenModel> CazadorNen { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CazadorModel>(t =>
            {
                t.HasKey(c => c.Id);
                t.Property(c => c.Nombre).HasColumnType("VARCHAR(50)");
            });

            modelBuilder.Entity<NenModel>(t => { 
                t.HasKey(c => c.Id);
                t.Property(c => c.Nombre).HasColumnType("VARCHAR(50)");
                t.Property(c => c.Descripcion).HasColumnType("VARCHAR(256)");
            });

            modelBuilder.Entity<CazadorNenModel>(t => {
                t.HasKey(c => new { c.Id_Cazador, c.Id_Nen });
                t.HasOne(c => c.Cazador).WithMany(c => c.Cazador_Nen).HasForeignKey(c => c.Id_Cazador).OnDelete(DeleteBehavior.Restrict);
                t.HasOne(c => c.Nen).WithMany(c => c.Cazador_Nen).HasForeignKey(c => c.Id_Nen).OnDelete(DeleteBehavior.Restrict);
            });

            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CazadorModel>().HasData(
                new CazadorModel { Id = 1, Nombre = "Gon Freecss", Edad = 12 },
                new CazadorModel { Id = 2, Nombre = "Killua Zoldyck", Edad = 12 },
                new CazadorModel { Id = 3, Nombre = "Kurapika Kurta", Edad = 17 },
                new CazadorModel { Id = 4, Nombre = "Leorio Paladiknight", Edad = 19 }
            );

            modelBuilder.Entity<NenModel>().HasData(
                new NenModel { Id = 1, Nombre = "Intensificación", Descripcion = "Si un estudiante aumenta la cantidad de agua en el vaso durante su prueba del agua, es de Intensificación" },
                new NenModel { Id = 2, Nombre = "Transformación", Descripcion = "Si un estudiante cambia el sabor del agua durante su prueba del agua es un Transformador" },
                new NenModel { Id = 3, Nombre = "Materialización", Descripcion = "Si un estudiante hace aparecer impurezas en el agua del vaso durante su prueba ellos son Materialización" },
                new NenModel { Id = 4, Nombre = "Emisión", Descripcion = "Si un estudiante cambia el color del agua en el vaso durante su prueba del agua, es un Emisor" },
                new NenModel { Id = 5, Nombre = "Manipulación", Descripcion = "Si un estudiante mueve la hoja flotando en el agua del vaso durante su prueba del agua, es un Manipulador" },
                new NenModel { Id = 6, Nombre = "Especialización", Descripcion = "Si un estudiante hace algún otro efecto durante su prueba del agua, son Especialistas" }
            );

            modelBuilder.Entity<CazadorNenModel>().HasData(
                new CazadorNenModel { Id_Cazador = 1, Id_Nen = 1 },
                new CazadorNenModel { Id_Cazador = 2, Id_Nen = 2 },
                new CazadorNenModel { Id_Cazador = 3, Id_Nen = 3 },
                new CazadorNenModel { Id_Cazador = 3, Id_Nen = 6 },
                new CazadorNenModel { Id_Cazador = 4, Id_Nen = 4 }
            );
        }
    }
}
