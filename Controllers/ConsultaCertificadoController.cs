using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReportesCabildoAwa.Models;
using ReportesCabildoAwa.Models.ViewModels;
using ReportesCabildoAwa.UnitOfWork;

namespace ReportesCabildoAwa.Controllers
{
    public class ConsultaCertificadoController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ConsultaCertificadoController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Consulta()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> BuscarDocumento(string tipoDocumento, string numeroDocumento)
        {
            if (string.IsNullOrEmpty(tipoDocumento) || string.IsNullOrEmpty(numeroDocumento))
            {
                ViewData["Error"] = "Por favor, ingrese el tipo y número de documento.";
                return View("Consulta");
            }

            var persona = await _unitOfWork.Repository<Persona>().FirstOrDefaultAsync(p => p.NumeroDocumento == numeroDocumento);

            if (persona == null)
            {
                ViewData["PersonaNoEncontrada"] = true;
                return View("Consulta");
            }

            ViewData["Persona"] = persona;
            return View("Consulta");
        }

        public async Task<IActionResult> Detalle(int id)
        {
            if (id <= 0)
                return NotFound();

            var certificado = await _unitOfWork.Repository<Persona>().GetById(id);

            if (certificado == null)
                return NotFound();

            var model = new PersonaViewModel
            {
                IdPersona = certificado.IdPersona,
                NumeroDocumento = certificado.NumeroDocumento,
                Nombre = certificado.Nombre,
                Apellido = certificado.Apellido,
                FechaNacimiento = certificado.FechaNacimiento,
                Direccion = certificado.Direccion,
                Telefono = certificado.Telefono,
                CorreoElectronico = certificado.CorreoElectronico
            };

            return View(model);
        }

        public IActionResult Crear()
        {
            var model = new PersonaViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(PersonaViewModel model)
        {
            if (ModelState.IsValid)
            {
                var certificado = new Persona
                {
                    NumeroDocumento = model.NumeroDocumento,
                    Nombre = model.Nombre,
                    Apellido = model.Apellido,
                    FechaNacimiento = model.FechaNacimiento,
                    Direccion = model.Direccion,
                    Telefono = model.Telefono,
                    CorreoElectronico = model.CorreoElectronico
                };

                await _unitOfWork.Repository<Persona>().Add(certificado);
                await _unitOfWork.Save();

                return RedirectToAction("Consulta");
            }

            return View(model);
        }

        public async Task<IActionResult> Editar(int id)
        {
            if (id <= 0)
                return NotFound();

            var certificado = await _unitOfWork.Repository<Persona>().GetById(id);

            if (certificado == null)
                return NotFound();

            var model = new PersonaViewModel
            {
                IdPersona = certificado.IdPersona,
                NumeroDocumento = certificado.NumeroDocumento,
                Nombre = certificado.Nombre,
                Apellido = certificado.Apellido,
                FechaNacimiento = certificado.FechaNacimiento,
                Direccion = certificado.Direccion,
                Telefono = certificado.Telefono,
                CorreoElectronico = certificado.CorreoElectronico
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(int id, PersonaViewModel model)
        {
            if (id != model.IdPersona)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var persona = await _unitOfWork.Repository<Persona>().GetById(id);
                    if (persona == null)
                        return NotFound();

                    persona.NumeroDocumento = model.NumeroDocumento;
                    persona.Nombre = model.Nombre;
                    persona.Apellido = model.Apellido;
                    persona.FechaNacimiento = model.FechaNacimiento;
                    persona.Direccion = model.Direccion;
                    persona.Telefono = model.Telefono;
                    persona.CorreoElectronico = model.CorreoElectronico;

                    _unitOfWork.Repository<Persona>().Update(persona);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await _unitOfWork.Repository<Persona>().GetById(model.IdPersona) == null)
                    {
                        return NotFound();
                    }
                    else
                        throw;
                }

                return RedirectToAction("Consulta");
            }

            return View(model);
        }

        public async Task<IActionResult> Eliminar(int? id)
        {
            if (id == null)
                return NotFound();

            var persona = await _unitOfWork.Repository<Persona>().GetById((int)id);

            if (persona == null)
                return NotFound();

            var model = new PersonaViewModel
            {
                IdPersona = persona.IdPersona,
                NumeroDocumento = persona.NumeroDocumento,
                Nombre = persona.Nombre,
                Apellido = persona.Apellido,
                FechaNacimiento = persona.FechaNacimiento,
                Direccion = persona.Direccion,
                Telefono = persona.Telefono,
                CorreoElectronico = persona.CorreoElectronico
            };

            return View(model);
        }

        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarConfirmado(int id)
        {
            var persona = await _unitOfWork.Repository<Persona>().GetById(id);
            if (persona != null)
            {
                _unitOfWork.Repository<Persona>().Delete(persona);
                await _unitOfWork.Save();
            }

            return RedirectToAction("Consulta");
        }
    }
}
