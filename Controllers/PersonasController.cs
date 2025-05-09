using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReportesCabildoAwa.Business;
using ReportesCabildoAwa.Common.Constantes;
using ReportesCabildoAwa.Common.Paginacion;
using ReportesCabildoAwa.Models;
using ReportesCabildoAwa.Models.ViewModels;

namespace ReportesCabildoAwa.Controllers
{
    public class PersonasController : Controller
    {
        private readonly PersonaBusiness _personaBusiness;

        public PersonasController(PersonaBusiness personaBusiness)
        {
            _personaBusiness = personaBusiness;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int page = 1, int pageSize = PaginacionConstantes.TamañoPorDefecto)
        {
            // Validar si el pageSize es uno de los permitidos
            if (!PaginacionConstantes.TamañosPermitidos.Contains(pageSize))
            {
                pageSize = PaginacionConstantes.TamañoPorDefecto;
            }
    
            var paginacion = new PaginationParams
            {
                PageNumber = page,
                PageSize = pageSize
            };

        var resultado = await _personaBusiness.ObtenerPaginadoAsync(paginacion);
        return View(resultado);
    }



    [HttpGet]
    public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var persona = await _personaBusiness.ObtenerPorIdAsync(id.Value);
            if (persona == null) return NotFound();

            return View(persona);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["IdTipoDocumento"] = new SelectList(await _personaBusiness.ObtenerTiposDocumentoAsync(), "IdTipoDocumento", "Nombre");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PersonaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewData["IdTipoDocumento"] = new SelectList(await _personaBusiness.ObtenerTiposDocumentoAsync(), "IdTipoDocumento", "Nombre", model.IdTipoDocumento);
                return View(model);
            }

            var persona = MapearAPersona(model);
            await _personaBusiness.CrearAsync(persona);

            return RedirectToAction(nameof(Index));
        }

        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var persona = await _personaBusiness.ObtenerPorIdAsync(id.Value);
            if (persona == null) return NotFound();

            ViewData["IdTipoDocumento"] = new SelectList(await _personaBusiness.ObtenerTiposDocumentoAsync(), "IdTipoDocumento", "Nombre", persona.IdTipoDocumento);
            return View(persona);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Persona persona)
        {
            if (id != persona.IdPersona) return NotFound();

            //if (!ModelState.IsValid)
            //{
            //    ViewData["IdTipoDocumento"] = new SelectList(await _personaBusiness.ObtenerTiposDocumentoAsync(), "IdTipoDocumento", "Nombre", persona.IdTipoDocumento);
            //    return View(persona);
            //}

            try
            {
                await _personaBusiness.ActualizarAsync(persona);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_personaBusiness.Existe(persona.IdPersona)) return NotFound();
                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var persona = await _personaBusiness.ObtenerPorIdAsync(id.Value);
            if (persona == null) return NotFound();

            return View(persona);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var persona = await _personaBusiness.ObtenerPorIdAsync(id);
            if (persona != null)
            {
                await _personaBusiness.EliminarAsync(persona);
            }

            return RedirectToAction(nameof(Index));
        }

        private Persona MapearAPersona(PersonaViewModel model) => new()
        {
            IdPersona = model.IdPersona,
            NumeroDocumento = model.NumeroDocumento,
            Nombre = model.Nombre,
            Apellido = model.Apellido,
            FechaNacimiento = model.FechaNacimiento,
            Direccion = model.Direccion,
            Telefono = model.Telefono,
            CorreoElectronico = model.CorreoElectronico,
            IdTipoDocumento = model.IdTipoDocumento,
            EstadoPersona = model.EstadoPersona
        };
    }
}
