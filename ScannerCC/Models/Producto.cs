using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace ScannerCC.Models
{
    public class Producto
    {
        [Key]
        public int idProducto { get; set; }

        public string CodigoBarra { get; set; }
        public string Nombre { get; set; }
        public string Cepa{ get; set; }
        public string? PaisOrigen { get; set; }
        public string? PaisDestino { get; set; }
        public DateTime? FechaCosecha { get; set; }
        public DateTime? FechaProduccion { get; set; }
        public int? Capacidad { get; set; }
        public float? GradoAlcohol { get; set; }
        public float? Azucar { get; set; }
        public float? Sulfuroso { get; set; }
        public float? Densidad { get; set; }
        public string? TipoCapsula { get; set; }
        public string? TipoEtiqueta { get; set; }
        public string? ColorBotella { get; set; }
        public bool? Medalla { get; set;}
        public string? ColorCapsula { get; set; }
        public string? TipoCorcho { get; set; }
        public string? TipoBotella { get; set; }
        public float AlturaBotella { get; set; }
        public float AnchoBotella { get; set; }
        public float MedidadEtiquetaABoquete { get; set; }
        public float MedidaEtiquetaABase { get; set; }
    }
}
