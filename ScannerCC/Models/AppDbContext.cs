using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ScannerCC.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Producto> Producto { get; set; }
        public DbSet<Escaneo> Escaneo { get; set; }
        public DbSet<Rol> Rol { get; set; }
        public DbSet<UsuarioProducto> UsuarioProducto { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\ProjectModels;Initial Catalog=Scanner;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }

    }
}