using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics.Metrics;

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
            optionsBuilder.UseSqlServer("Server = localhost\\SQLEXPRESS; Database =scanner;User Id=EC2AMAZ-3AAN22G\\Adminstrator; Integrated Security=True;TrustServerCertificate=true;");
        }




    }
}