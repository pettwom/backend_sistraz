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
        public async Task<List<EntidadDto>> ObtenerListadoEntidadAsync(EntidadDto edt)
        {
            System.Diagnostics.Debug.WriteLine("================================");
            var actividades = new[] { 16m, 52m };
            var datos = await _context.VusrInfHydroGeneralSps
                .AsNoTracking()
                .Where(sp => sp.IdActividad == 16 || sp.IdActividad == 52)
                .ToListAsync();

            var lista = datos.Select(sp => new EntidadDto
            {
                IdEntidad = decimal.ToInt64(sp.IdEntidad),
                IdActividad = sp.IdActividad,
                IdDepartamento = sp.IdDepartamento,
                IdMunicipio = sp.IdMunicipio,
                Denominacion = sp.Denominacion,
                Actividad = sp.Actividad,
                Telefonos = sp.Telefonos?.ToString(),
                AmbitoOperacion = sp.AmbitoOperacion,
                Direccion = sp.Direccion,
                Departamento = sp.Departamento,
                Municipio = sp.Municipio
            }).ToList();

            return lista;
        }
    }
}
