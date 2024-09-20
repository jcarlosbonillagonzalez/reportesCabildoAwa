using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ReportesCabildoAwa.Models
{
    public class Persona
    {
        [Key]
        public int IdPersona { get; set; }

        [Required]
        [DisplayName("Numero Documento")]
        public string NumeroDocumento { get; set; }

        [Required]
        [DisplayName("Nombre")]
        [StringLength(80)]
        public string Nombre { get; set; }

        [Required]
        [DisplayName("Apellido")]
        [StringLength(80)]
        public string Apellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string CorreoElectronico { get; set; }
    }
}
