using System.ComponentModel.DataAnnotations;

namespace ReportesCabildoAwa.Models
{
    public class TipoDocumento
    {
        [Key]
        [Display(Name = "Id Tipo Documento")]
        public int IdTipoDocumento { get; set; }

        [Required]
        [Display(Name = "Nombre Tipo Documento")]
        public string Nombre { get; set; }

        [Required]
        [Display(Name = "Estado Tipo Documento")]
        public bool EstadoTipoDocumento { get; set; }
    }
}
