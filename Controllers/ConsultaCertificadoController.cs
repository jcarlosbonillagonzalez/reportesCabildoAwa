using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ReportesCabildoAwa.Controllers
{
    public class ConsultaCertificadoController : Controller
    {
        public IActionResult Consulta()
        {
            return View();
        }
    }
}

