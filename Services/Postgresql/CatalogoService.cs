using backend_trazabilidad.DTOs.Oracle;
using backend_trazabilidad.DTOs.Postgresql;
using backend_trazabilidad.Models;
using Microsoft.EntityFrameworkCore;
using OracleScanffold.Models.Oracle;
using System.Linq; // <- añadido

namespace backend_trazabilidad.Services.Postgresql
{
    public class CatalogoService : ICatalogoService
    {
        private readonly PostgresDbContext _context;
        private readonly AplicationDbContext _contextOracle;
        public CatalogoService(PostgresDbContext context, AplicationDbContext contextOracle)
        {
            _context = context;
            _contextOracle = contextOracle;
        }

        public async Task<List<SelectOptionDto>> ObtenerParametricasAsync()
        {
            return await
                (
                    from ti in _context.TbInstancia.AsNoTracking()
                    join tp in _context.TbPlanta.AsNoTracking()
                    on ti.IdInstancia equals tp.IdInstancia
                    where tp.TipoOperacion == 1
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

        public async Task<List<PaisDto>> ObtenerPaisAsync()
        {
            return await _contextOracle.Paises.AsNoTracking()
                .Where(p => p.AudEstado != 3)
                .OrderBy(p=> p.Descripcion)
                .Select(p=> new PaisDto 
                { 
                    IdPais = p.IdPais,
                    Descripcion = p.Descripcion,
                    Abreviacion2 = p.Abreviacion2,
                    Abreviacion3 = p.Abreviacion3
                })
                .ToListAsync();
        }
    }
}