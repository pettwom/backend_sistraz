using backend_trazabilidad.DTOs.Oracle;
using backend_trazabilidad.Models.Oracle;
using DocumentFormat.OpenXml.Office2010.Drawing;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace backend_trazabilidad.Services.Octano
{
    public class EntidadServices:IEntidadServies
    {
        private readonly AplicationDbContext _context; 
        public EntidadServices(AplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<EntidadDto>> ObtenerListadoEntidadLocalAsync()
        {
            var datos = await _context.VusrInfHydroGeneralSps
                .AsNoTracking()
                .Where(sp => sp.IdActividad == 52)
                .OrderBy(sp=> sp.Denominacion)
                .ToListAsync();

            var lista = datos.Select(sp => new EntidadDto
            {
                IdEntidad = decimal.ToInt64(sp.IdEntidad),
                IdActividad = sp.IdActividad,
                Denominacion = sp.Denominacion,
                Actividad = sp.Actividad
            }).ToList();

            return lista;
        }
        public async Task<List<EntidadDto>> ObtenerListadoEntidadImportacionAsync()
        {
            var datos = await _context.VusrInfHydroGeneralSps
                .AsNoTracking()
                .Where(sp => sp.IdActividad == 16)
                .OrderBy(sp => sp.Denominacion)
                .ToListAsync();

            var lista = datos.Select(sp => new EntidadDto
            {
                IdEntidad = decimal.ToInt64(sp.IdEntidad),
                IdActividad = sp.IdActividad,
                Denominacion = sp.Denominacion,
                Actividad = sp.Actividad
            }).ToList();

            return lista;
        }
    }
}
