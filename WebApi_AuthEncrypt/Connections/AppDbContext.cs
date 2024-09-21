using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WebApi_AuthEncrypt.Models.Entities;

namespace WebApi_AuthEncrypt.Connections
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<UserEntity> Usuarios { get; set; }
        public DbSet<DataEntity> Data { get; set; }
    }
}
