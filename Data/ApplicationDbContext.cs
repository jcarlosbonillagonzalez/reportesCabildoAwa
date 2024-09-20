using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ReportesCabildoAwa.Models;

namespace ReportesCabildoAwa.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Persona> Personas { get; set; }
        public DbSet<TipoDocumento> TipoDocumentos { get; set; }
    
    }
    
}
