using System.ComponentModel.DataAnnotations;
using System.Drawing;


namespace ScannerCC.Models
{
    public class Controles
    {
        [Key]
        public int idControl { get; set; }

        public int ProductoId { get; set; }
        public Producto Producto { get; set; }

        public string Linea { get; set; }
        public string Cepa { get; set; }
        public string? PaisOrigen { get; set; }
        public string? PaisDestino { get; set; }
        public string? Comentario { get; set; }
        public bool? Tipodecontrol { get; set; }
    }
}
