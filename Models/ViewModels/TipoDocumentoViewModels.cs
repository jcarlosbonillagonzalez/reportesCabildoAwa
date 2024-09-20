using System.ComponentModel.DataAnnotations;

namespace ReportesCabildoAwa.Models.ViewModels
{
    public class TipoDocumentoViewModels
    {
        public int IdTipoDocumento { get; set; }
        public string Nombre { get; set; }
        public bool EstadoTipoDocumento { get; set; }
    }
}
