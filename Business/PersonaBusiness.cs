using Microsoft.EntityFrameworkCore;
using ReportesCabildoAwa.Common.Paginacion;
using ReportesCabildoAwa.Data;
using ReportesCabildoAwa.Models;

namespace ReportesCabildoAwa.Business
{
    public class PersonaBusiness
    {
        private readonly ApplicationDbContext _context;

        public PersonaBusiness(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<Persona>> ObtenerPaginadoAsync(PaginationParams pag)
        {
            var query = _context.Personas.Include(p => p.TipoDocumento).AsQueryable();

            var totalItems = await query.CountAsync();
            var items = await query
                .Skip((pag.PageNumber - 1) * pag.PageSize)
                .Take(pag.PageSize)
                .ToListAsync();

            return new PagedResult<Persona>
            {
                Items = items,
                TotalItems = totalItems,
                PageNumber = pag.PageNumber,
                PageSize = pag.PageSize
            };
        }

        public async Task<List<Persona>> ObtenerTodasAsync()
        {
            return await _context.Personas.Include(p => p.TipoDocumento).ToListAsync();
        }

        public async Task<Persona> ObtenerPorIdAsync(int id)
        {
            return await _context.Personas
                .Include(p => p.TipoDocumento)
                .FirstOrDefaultAsync(p => p.IdPersona == id);
        }

        public async Task CrearAsync(Persona persona)
        {
            await _context.Personas.AddAsync(persona);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarAsync(Persona persona)
        {
            _context.Personas.Update(persona);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(Persona persona)
        {
            _context.Personas.Remove(persona);
            await _context.SaveChangesAsync();
        }

        public async Task<List<TipoDocumento>> ObtenerTiposDocumentoAsync()
        {
            return await _context.TipoDocumentos.ToListAsync();
        }

        public bool Existe(int id)
        {
            return _context.Personas.Any(p => p.IdPersona == id);
        }
    }
}
