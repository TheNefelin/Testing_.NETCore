using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApi.EntityFramework.Models;

public partial class DbTestingContext : DbContext
{
    public DbTestingContext()
    {
    }

    public DbTestingContext(DbContextOptions<DbTestingContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Hunter> Hunters { get; set; }

    public virtual DbSet<Nen> Nens { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost; Database=db_testing; User ID=testing; Password=testing; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Hunter>(entity =>
        {
            entity.ToTable("Hunter");

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasMany(d => d.Nens).WithMany(p => p.Hunters)
                .UsingEntity<Dictionary<string, object>>(
                    "HunterNen",
                    r => r.HasOne<Nen>().WithMany()
                        .HasForeignKey("NenId")
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    l => l.HasOne<Hunter>().WithMany()
                        .HasForeignKey("HunterId")
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    j =>
                    {
                        j.HasKey("HunterId", "NenId");
                        j.ToTable("Hunter_Nen");
                        j.HasIndex(new[] { "NenId" }, "IX_Hunter_Nen_Nen_Id");
                        j.IndexerProperty<int>("HunterId").HasColumnName("Hunter_Id");
                        j.IndexerProperty<int>("NenId").HasColumnName("Nen_Id");
                    });
        });

        modelBuilder.Entity<Nen>(entity =>
        {
            entity.ToTable("Nen");

            entity.Property(e => e.Description)
                .HasMaxLength(256)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
