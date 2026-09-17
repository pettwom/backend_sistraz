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
            return await
                (
                    from ti in _context.TbInstancia.AsNoTracking()
                    join tp in _context.TbPlanta.AsNoTracking()
                    on ti.IdInstancia equals tp.IdInstancia
                    orderby ti.Codigo
                    select new SelectOptionDto
                    {
                        IdPlanta = tp.IdPlanta,
                        Nombre = (ti.Nombre ?? string.Empty).ToUpper(),
                        Codigo = (ti.Codigo ?? string.Empty).ToUpper(),
                        Departamento = tp.Departamento ?? string.Empty
                    }
                )
                .ToListAsync();
        }
    }
}