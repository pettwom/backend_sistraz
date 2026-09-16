using backend_trazabilidad.DTOs.Postgresql;
using backend_trazabilidad.Models.Postgresql;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace backend_trazabilidad.Services.Postgresql
{
    public class ProduccionService : IProduccionService
    {
        private readonly PostgresDbContext _context;
        private readonly ILogger<ProduccionService> _logger;
        private IHttpContextAccessor _httpContextAccessor;
        public ProduccionService(PostgresDbContext context, ILogger<ProduccionService> logger, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
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

        public async Task<CrearPlantaDto> CrearPlantaAsync(CrearPlantaDto plt)
        {
            try
            {
                await using var transaccion = await _context.Database.BeginTransactionAsync();
                // ================================================================================
                // 1. BUSCAR PLANTA
                // ================================================================================
                var planta = await _context.TbInstancia.FirstOrDefaultAsync(x => x.Codigo == plt.CodPlanta);
                // ================================================================================
                // 1. CREAR NUEVA INSTANCIA
                // ================================================================================
                var instancia = new TbInstancium
                {
                    Codigo = plt.CodPlanta,
                    Descripcion = plt.DescPlanta,
                    IdTipoLugar = 1,
                    Ubicacion = "BOLIVIA",
                    Estado = true,
                    Usucre = ObtenerIdUsuario()
                };
                _context.TbInstancia.Add(instancia);
                await _context.SaveChangesAsync();
                // ================================================================================
                // 2. CREAR NUEVA PLANTA
                // ================================================================================
                var plant = new TbPlantum
                {
                    IdInstancia = instancia.IdInstancia,
                    TipoOperacion = 1,
                    Pais = plt.Pais,
                    Departamento = plt.Departamento,
                    PuntoIngreso = plt.PuntoIngreso,
                    Estado = true,
                    Observacion = plt.ObsPlanta
                };
                _context.TbPlanta.Add(plant);
                await _context.SaveChangesAsync();

                await transaccion.CommitAsync();
                // ================================================================================
                // 3. RETORNA RESULTADO
                // ================================================================================
                return plt;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR AL CREAR PLANTA: {Mensaje}", ex.Message);

                throw;
            }

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
        //            Codigo = GenerarCodigoTrazabilidad(),
        //            IdPlantaOrigen = planta.IdPlanta,
        //            FechaOrigen = dto.FechaMuestra,
        //            VolumenInicial = dto.VolTotal,
        //            Unidad = "Tn",
        //            Estado = "Activo",
        //            Activo = true,
        //            CreadoEn = DateTime.Now,
        //            IdCreadoPor = ObtenerIdUsuario(),
        //            CreadoPor = ObtenerUsername()
        //        };
        //        _context.TbLoteGlps.Add(lote);
        //        await _context.SaveChangesAsync();

        //        // ================================================================================
        //        // 3. CREAR EVENTO
        //        // ================================================================================
        //        var evento = new TbEvento
        //        { 

        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}
        private string GenerarCodigoTrazabilidad()
        {
            return $"TRZ-{DateTime.Now:yyyyMMdd}-{Guid.NewGuid()
                .ToString("N")[..6]
                .ToUpper()}";
        }
        private long ObtenerIdUsuario()
        {
            var usuario = _httpContextAccessor.HttpContext?.User;

            if (usuario == null)
                throw new UnauthorizedAccessException(
                    "No existe un usuario autenticado."
                );

            foreach (var claim in usuario.Claims)
            {
                Console.WriteLine(
                    $"CLAIM => {claim.Type} = {claim.Value}"
                );
            }

            var idUsuarioClaim =
                usuario.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            Console.WriteLine(
                $"ID USUARIO CLAIM => {idUsuarioClaim}"
            );

            if (!long.TryParse(idUsuarioClaim, out var idUsuario))
            {
                throw new UnauthorizedAccessException(
                    "No se pudo obtener el ID del usuario."
                );
            }

            return idUsuario;
        }
        private string ObtenerUsername()
        {
            var usuario = _httpContextAccessor
                .HttpContext?
                .User;

            if (usuario == null)
            {
                throw new UnauthorizedAccessException(
                    "No existe un usuario autenticado."
                );
            }

            var username = usuario
                .FindFirst(ClaimTypes.Name)?
                .Value;

            if (string.IsNullOrEmpty(username))
            {
                throw new UnauthorizedAccessException(
                    "No se pudo obtener el nombre de usuario."
                );
            }

            return username;
        }
    }
}
