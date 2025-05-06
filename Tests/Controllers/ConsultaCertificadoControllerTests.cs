using Microsoft.AspNetCore.Mvc;
using Moq;
using ReportesCabildoAwa.Controllers;
using ReportesCabildoAwa.Models;
using ReportesCabildoAwa.Models.ViewModels;
using ReportesCabildoAwa.UnitOfWork;
using Xunit;

namespace ReportesCabildoAwa.Tests.Controllers
{
    public class ConsultaCertificadoControllerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly ConsultaCertificadoController _controller;

        public ConsultaCertificadoControllerTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _controller = new ConsultaCertificadoController(_mockUnitOfWork.Object);
        }

        [Fact]
        public async Task Crear_ConModeloValido_DebeRedirectAConsulta()
        {
            // Arrange
            var personaViewModel = new PersonaViewModel
            {
                NumeroDocumento = "123456789",
                Nombre = "Juan",
                Apellido = "Pérez",
                FechaNacimiento = DateTime.Now.AddYears(-20),
                Direccion = "Calle 123",
                Telefono = "1234567890",
                CorreoElectronico = "juan@ejemplo.com"
            };

            var mockRepository = new Mock<IGenericRepository<Persona>>();
            _mockUnitOfWork.Setup(uow => uow.Repository<Persona>())
                          .Returns((Repository.IRepository<Persona>)mockRepository.Object);

            // Act
            var resultado = await _controller.Crear(personaViewModel);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(resultado);
            Assert.Equal("Consulta", redirectToActionResult.ActionName);

            _mockUnitOfWork.Verify(uow => uow.Repository<Persona>().Add(It.IsAny<Persona>()), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.Save(), Times.Once);
        }

        [Fact]
        public async Task Crear_ConModeloInvalido_DebeRegresarVistaConModelo()
        {
            // Arrange
            var personaViewModel = new PersonaViewModel();
            _controller.ModelState.AddModelError("Error", "Error de validación");

            // Act
            var resultado = await _controller.Crear(personaViewModel);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(resultado);
            Assert.IsType<PersonaViewModel>(viewResult.Model);

            _mockUnitOfWork.Verify(uow => uow.Repository<Persona>().Add(It.IsAny<Persona>()), Times.Never);
            _mockUnitOfWork.Verify(uow => uow.Save(), Times.Never);
        }
    }

    internal interface IGenericRepository<T>
    {
    }
}