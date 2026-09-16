using backend_trazabilidad.DTOs.Postgresql;
using backend_trazabilidad.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq; // <- añadido

namespace backend_trazabilidad.Services.Postgresql
{
    public class CatalogoService : ICatalogoService
    {
        private readonly PostgresDbContext _context;
        public CatalogoService(PostgresDbContext context)
        {
            _context = context;
        }

        public async Task<List<SelectOptionDto>> ObtenerParametricasAsync() 
        {
            return await _context.TblParametricas
                .AsNoTracking()
                .Where(x => x.Estado == true)
                .OrderBy(x => x.Id) 
                .Select(x => new SelectOptionDto
                {
                    Id = x.Id, 
                    Nombre = x.Nombre ?? string.Empty, 
                    Descripcion = x.Descripcion ?? string.Empty,
                    Valor = x.Valor ?? string.Empty,
                    Estado = (short)(x.Estado ? 1 : 0), 
                    Categoria = x.Categoria ?? string.Empty
                })
                .ToListAsync();
        }
    }
}