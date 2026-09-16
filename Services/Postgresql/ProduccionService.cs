using backend_trazabilidad.DTOs.Postgresql;
using backend_trazabilidad.Models.Postgresql;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace backend_trazabilidad.Services.Postgresql
{
    public class ProduccionService:IProduccionService
    {
        private readonly PostgresDbContext _context;
        private readonly ILogger<ProduccionService> _logger;
        public ProduccionService(PostgresDbContext context, ILogger<ProduccionService> logger)
        {
            _context = context;
            _logger = logger;
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

        //public async Task<CrearProduccionRequestDto> CrearProdAsync(CrearProduccionRequestDto dto) 
        //{
        //    await using var transaccion = await _context.Database.BeginTransactionAsync();
        //    try 
        //    {
        //        // ================================================================================
        //        // 1. BUSCAR PLANTA
        //        // ================================================================================
        //        var planta = await _context.TbPlanta.FirstOrDefaultAsync(x => x.IdPlanta == dto.PlantaId);// obtengo los datos de la planta

        //        _logger.LogInformation("1. planta = ", planta);

        //        if (planta == null) throw new Exception("La Planta no existe");
        //        if (planta.IdInstancia == null) throw new Exception("La Planta no tiene una instancia asociada");

        //        // ================================================================================
        //        // 2. CREAR LOTE
        //        // ================================================================================
        //        var lote = new TbLoteGlp 
        //        { 
        //        Codigo = GenerarCodigoTrazabilidad(),
        //        IdPlantaOrigen = planta.IdPlanta,
        //        FechaOrigen = dto.FechaMuestra,
        //        VolumenInicial = dto.VolTotal,
        //        Unidad = "Tn",
        //        Estado = true,
        //        Activo = true,
        //        CreadoEn = DateTime,
        //        IdCreadoPor = 
        //        }

        //    }
        //    catch (Exception ex) 
        //    { 
            
        //    }
        //}
    }
}
