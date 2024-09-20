using Microsoft.IdentityModel.Tokens;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ReportesCabildoAwa.Models
{
    public class Personas
    {
        [Key]
        public Guid IdPersona { get; set; }

        [DisplayName("Numero Documento")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string NumeroDocumento { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string CorreoElectronico { get; set; }
    }
}
