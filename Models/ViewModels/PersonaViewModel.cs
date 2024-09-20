using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ReportesCabildoAwa.Models.ViewModels
{
    public class PersonaViewModel
    {
        public int IdPersona { get; set; }
        public string NumeroDocumento { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string CorreoElectronico { get; set; }
    }
}
