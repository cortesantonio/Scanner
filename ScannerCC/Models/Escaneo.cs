using ScannerCC.Models;
using System.ComponentModel.DataAnnotations;

namespace ScannerCC.Models
{
    public class Escaneo
    {
        [Key]
        public int IdEscaneo { get; set; }
        public int EscaneoId { get; set; }
        public int ProductoId { get; set; }
        public Producto Producto { get; set; }

        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public DateTime Fecha { get; set; }

        
    }
}


