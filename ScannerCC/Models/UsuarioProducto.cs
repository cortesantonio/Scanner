using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ScannerCC.Models;
using System.ComponentModel.DataAnnotations;

namespace ScannerCC.Models
{
    public class UsuarioProducto
    {
        [Key]
        public string IdUsuarioProducto { get; set; }
        public int ProductoId { get; set; }
        public Producto Producto { get; set; }

        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime Hora { get; set; }
    }
}

    