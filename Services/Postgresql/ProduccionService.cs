using backend_trazabilidad.DTOs.Postgresql;
using backend_trazabilidad.Models.Postgresql;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace backend_trazabilidad.Services.Postgresql
{
    public class ProduccionService:IProduccionService
    {
        private readonly PostgresDbContext _context;
        public ProduccionService(PostgresDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProduccionDto>> ObtenerListadoAsync()
        {
            var lista = await (
                from tp in _context.TbPlanta

                join tlg in _context.TbLoteGlps
                on tp.IdPlanta equals tlg.IdPlantaOrigen

                join ti in _context.TbInstancia
                on tp.IdInstancia equals ti.IdInstancia

                join teo in _context.TbEventoOrigens
                on ti.IdInstancia equals teo.IdInstancia

                join te in _context.TbEventos
                on teo.IdEvento equals te.IdEvento

                join tc in _context.TbCertificados
                on te.IdEvento equals tc.IdEvento

                where ti.IdTipoLugar == 1

                orderby tlg.Codigo ascending

                select new ProduccionDto
                {
                    Lote = tlg.Codigo,
                    Nombre = ti.Codigo,
                    NumCertificado = tc.NumeroCertificado,
                    FechaMuestreo = tlg.FechaOrigen,
                    VolTotal = tlg.VolumenInicial,
                    Pais = tp.Pais,
                    PuntoIngreso = tp.PuntoIngreso,
                    Estado = tlg.Estado,
                    TipoOperacion = tp.TipoOperacion
                }
                ).AsNoTracking().ToListAsync();
            return lista;
        }
    }
}
