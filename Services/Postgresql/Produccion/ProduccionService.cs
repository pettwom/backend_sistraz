using backend_trazabilidad.DTOs.Postgresql;
using backend_trazabilidad.Models.Postgresql;
using Microsoft.EntityFrameworkCore;

namespace backend_trazabilidad.Services.Postgresql.Produccion
{
    public class ProduccionService : IProduccionService
    {
        private readonly PostgresDbContext _context;
        public ProduccionService(PostgresDbContext context)
        {
            _context = context;
        }


        public async Task<List<ProduccionDto>> ObtenerProduccionAsync()
        {
            var datos = await (
                from tt in _context.TblTrazabilidads

                join pr in _context.Produccions
                on tt.CorrOri equals pr.Correlativo

                join p2 in _context.Planta
                on pr.IdPlanta equals p2.Id
                select new
                {
                    tt.MesAnio,
                    tt.CorrOri,
                    tt.CorrDest,
                    tt.Lugar,

                    pr.NumCertCal,
                    pr.FechaMuestreo,

                    p2.VolTotal,
                    p2.Nombre,
                    p2.TipoOp,
                    p2.Pais,
                    p2.PuntoIngreso
                }
            ).ToListAsync();
            var resultado = datos
            .Select(x => new ProduccionDto
            {
                Lote =
                    $"TRZ-{x.MesAnio}-" +
                    $"{x.CorrOri:D5}-" +
                    $"{x.CorrDest:D5}-" +
                    $"{x.Lugar}",

                NumCertificacion =
                    x.NumCertCal ?? string.Empty,

                FechaMuestreo =
                    x.FechaMuestreo,

                VolTotal =
                    x.VolTotal,

                Nombre =
                    x.Nombre ?? string.Empty,

                Tipo = x.TipoOp switch
                {
                    1 => "Planta Local",
                    2 => "Importacion",
                    _ => string.Empty
                },

                Pais =
                    x.Pais ?? string.Empty,

                PuntoIngreso =
                    x.PuntoIngreso ?? string.Empty
            })
            .ToList();

            return resultado;


        }

    }
}
