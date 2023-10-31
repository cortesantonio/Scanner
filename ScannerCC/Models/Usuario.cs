using System.ComponentModel.DataAnnotations;

namespace ScannerCC.Models
{
    public class Usuario    
    {
        [Key]
        public int idUsuario { get; set; }

        public string? Nombre { get; set; }
        public string? Rut { get; set; }

        public string? Email { get; set; }
        public int RolId { get; set; }
        public Rol Rol { get; set; }

        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }

    }
}

