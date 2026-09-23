using backend_trazabilidad.DTOs.Postgresql;
using backend_trazabilidad.Models.Postgresql;
using Microsoft.EntityFrameworkCore;

namespace backend_trazabilidad.Services.Postgresql
{
    public class ExcelService:IExcelService
    {
        private readonly PostgresDbContext _context; 
        public ExcelService(PostgresDbContext context)
        {
            _context = context;
        }

        public async Task<List<CisternaExcelDto>> ObtenerListadoAsync() 
        {
            var listado = await (
                    from a in _context.TbCisternaDetalles.AsNoTracking()
                    orderby a.Fecha
                    select new CisternaExcelDto
                    {
                        Fecha = a.Fecha ?? DateTime.MinValue,
                        NroCre = a.NroCre ?? string.Empty,
                        NroCreFenix = a.NroCreFenix ?? string.Empty,
                        Cliente = a.Cliente ?? string.Empty,
                        PlantaDescarga = a.PlantaDescarga ?? string.Empty,
                        Conductor = a.Conductor ?? string.Empty,
                        Placa = a.Placa ?? string.Empty,
                        EmpresaTrans = a.EmpresaTrans ?? string.Empty,
                        Estado = a.Estado ?? string.Empty,
                        Id = a.Id
                    }
                ).ToListAsync();
            return listado;
        }
        public async Task<List<CisternaExcelDto>> ObtenerListSelectAsync()
        {
            var list = await (
                    from a in _context.TbCisternaDetalles.AsNoTracking()
                    group a by new { a.Placa, a.Conductor, a.VolBbls, a.Id } into g
                    select new CisternaExcelDto
                    {
                        Conductor = g.Key.Conductor ?? string.Empty,
                        Placa = g.Key.Placa ?? string.Empty,
                        VolBbls = (long) (g.Key.VolBbls),
                        Id= g.Key.Id
                    }
                ).ToListAsync();
            return list;
        }
    }
}
