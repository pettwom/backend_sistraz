using backend_trazabilidad.DTOs.Postgresql;
using backend_trazabilidad.Models.Postgresql;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Npgsql;
using System.Numerics;
using System.Security.Claims;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace backend_trazabilidad.Services.Postgresql
{
    public class ProduccionService : IProduccionService
    {
        private readonly PostgresDbContext _context;
        private readonly ILogger<ProduccionService> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEventoService _IEventoService;
        public ProduccionService(PostgresDbContext context, ILogger<ProduccionService> logger, IHttpContextAccessor httpContextAccessor, IEventoService IEventoService)
        {
            _context = context;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _IEventoService = IEventoService;
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
                on ti.IdInstancia equals teo.IdInstancia into teoGroup
                from teo in teoGroup.DefaultIfEmpty()

                join te in _context.TbEventos
                on teo.IdEvento equals te.IdEvento into teGroup
                from te in teGroup.DefaultIfEmpty()

                join tc in _context.TbCertificados
                on te.IdEvento equals tc.IdEvento into tcGroup
                from tc in tcGroup.DefaultIfEmpty()

                where tp.TipoOperacion == 1

                orderby tlg.Codigo ascending

                select new ProduccionDto
                {
                    Lote = tlg.Codigo,
                    Nombre = ti.Codigo,
                    NumCertificado = tc != null ? tc.NumeroCertificado : null,
                    FechaMuestreo = tlg.FechaOrigen,
                    VolTotal = tlg.VolumenInicial,
                    Pais = tp.Pais,
                    PuntoIngreso = tp.PuntoIngreso,
                    Estado = te.TipoEvento,
                    TipoOperacion = tp.TipoOperacion,
                    IdPlanta = tp.IdPlanta
                }
                ).AsNoTracking().ToListAsync();
            return lista;
        }

        public async Task<CrearPlantaDto> CrearPlantaAsync(CrearPlantaDto plt)
        {

            try
            {
                var usuario = ObtenerIdUsuario();

                System.Diagnostics.Debug.WriteLine($"ID USUARIO => {usuario.IdUsuario}");

                await using var transaccion = await _context.Database.BeginTransactionAsync();
                // ================================================================================
                // 1. BUSCAR PLANTA
                // ================================================================================
                var planta = await _context.TbInstancia.FirstOrDefaultAsync(x => x.Codigo == plt.CodPlanta);
                // ================================================================================
                // 2. CREAR NUEVA INSTANCIA
                // ================================================================================
                var instancia = new TbInstancium
                {
                    Codigo = plt.CodPlanta,
                    Nombre = plt.DescPlanta,
                    IdTipoLugar = 1,
                    Ubicacion = "BOLIVIA",
                    Estado = true,
                    //Usucre = usuario.IdUsuario
                };
                _context.TbInstancia.Add(instancia);
                await _context.SaveChangesAsync();
                // ================================================================================
                // 3. CREAR NUEVA PLANTA
                // ================================================================================
                var plant = new TbPlantum
                {
                    IdInstancia = instancia.IdInstancia,
                    TipoOperacion = 1,
                    Pais = "BOLIVIA",
                    Departamento = plt.Departamento,
                    PuntoIngreso = plt.PuntoIngreso,
                    Estado = true,
                    Observacion = plt.ObsPlanta,

                };
                _context.TbPlanta.Add(plant);
                await _context.SaveChangesAsync();

                await transaccion.CommitAsync();
                // ================================================================================
                // 4. RETORNA RESULTADO
                // ================================================================================
                return plt;
            }
            catch (DbUpdateException ex)
                when (ex.InnerException is PostgresException pgEx &&
                      pgEx.SqlState == PostgresErrorCodes.UniqueViolation &&
                      pgEx.ConstraintName == "uq_instancia_tipo_codigo")
            {
                throw new InvalidOperationException(
                    $"La planta con código '{plt.CodPlanta}' ya se encuentra registrada."
                );
            }

        }

        public async Task<CrearProduccionRequestDto> CrearProdAsync(CrearProduccionRequestDto dto)
        {

            try
            {
                var usuarioAutenticado = ObtenerIdUsuario();
                await using var transaccion = await _context.Database.BeginTransactionAsync();

                // ================================================================================
                // 1. BUSCAR PLANTA
                // ================================================================================

                var loteQwery = await _context.TbLoteGlps.FirstOrDefaultAsync(x => x.IdPlantaOrigen == dto.PlantaId);
                var plantaQwery = await _context.TbPlanta.FirstOrDefaultAsync(x => x.IdPlanta == dto.PlantaId);
                if (plantaQwery == null)
                {
                    throw new Exception(
                        $"No existe la planta con id_planta = {dto.PlantaId}"
                    );
                }
                if (loteQwery == null)
                {
                    // ================================================================================
                    // 2. ALMACENAR EN LOTE
                    // ================================================================================                
                    var loteSave = new TbLoteGlp
                    {
                        Codigo = GenerarCodigoTrazabilidad(),
                        IdPlantaOrigen = plantaQwery.IdPlanta,
                        FechaOrigen = SinZonaHoraria(dto.FechaMuestra),
                        VolumenInicial = dto.VolTotal,
                        Estado = "ACTIVO",
                        Activo = true,
                        CreadoEn = SinZonaHoraria(DateTime.Now),
                        IdCreadoPor = usuarioAutenticado.IdUsuario,
                        CreadoPor = usuarioAutenticado.Email
                    };
                    _context.TbLoteGlps.Add(loteSave);
                    await _context.SaveChangesAsync();
                    // ================================================================================
                    // 3. ALMACENAR EN EVENTO
                    // ================================================================================                
                    var eventoInit = new TbEvento
                    {
                        TipoEvento = "RECEPCION",
                        FechaEvento = SinZonaHoraria(DateTime.Now),
                        Estado = "CONFIRMADO",
                        Observacion = dto.Observacion
                        //Activo = true,
                        //CreadoEn = DateTime.Now,
                        //IdCreadoPor = usuarioAutenticado.IdUsuario,
                        //CreadoPor = usuarioAutenticado.Email
                    };
                    _context.TbEventos.Add(eventoInit);
                    await _context.SaveChangesAsync();
                    // ================================================================================
                    // 4. ALMACENAR EN EVENTO ORIGEN
                    // ================================================================================                
                    var eventoOrigen = new TbEventoOrigen
                    {
                        IdEvento = eventoInit.IdEvento,
                        IdInstancia = plantaQwery.IdInstancia,
                        IdLote = loteSave.IdLote,
                        Volumen = dto.VolTotal,
                        Observacion = dto.Observacion
                        //Estado = "ACTIVO",
                        //Activo = true,
                        //CreadoEn = DateTime.Now,
                        //IdCreadoPor = usuarioAutenticado.IdUsuario,
                        //CreadoPor = usuarioAutenticado.Email
                    };
                    _context.TbEventoOrigens.Add(eventoOrigen);
                    await _context.SaveChangesAsync();


                }
                // ================================================================================
                // 5. ALMACENO EN TABLA TBLOTEGLP LOS DATOS DE LOTESAVE
                // ================================================================================
                await transaccion.CommitAsync();
                // ================================================================================
                // 6. RETORNA RESULTADO
                // ================================================================================
                return dto;
            }
            catch (DbUpdateException ex)
                when (ex.InnerException is PostgresException pgEx &&
                      pgEx.SqlState == PostgresErrorCodes.UniqueViolation &&
                      pgEx.ConstraintName == "uq_instancia_tipo_codigo")
            {
                throw new InvalidOperationException(
                    $"El lote ya se encuentra registrada."
                );
            }
        }

        public async Task<CrearCisternasDto> CrearCisternasAsync(ProdCisternaDto pcd)
        {
            var y = 0;
            var usuarioAutenticado = ObtenerIdUsuario();
            TbInstancium? Instancia = null;
            await using var transaccion = await _context.Database.BeginTransactionAsync();

            // =============================================
            // BUSCAR PLANTA
            // =============================================

            var planta = await _context.TbPlanta.FirstOrDefaultAsync(x => x.IdPlanta == pcd.IdPlanta);

            if (planta == null)
            {
                System.Diagnostics.Debug.WriteLine($"NO EXISTE LA PLANTA {pcd.IdPlanta}");
                throw new Exception($"No existe la planta {pcd.IdPlanta}");
            }

            System.Diagnostics.Debug.WriteLine($"PLANTA ENCONTRADA: {planta.IdPlanta}");//✅
            System.Diagnostics.Debug.WriteLine($"ID INSTANCIA: {planta.IdInstancia}");//✅
            System.Diagnostics.Debug.WriteLine($"CISTERNAS: {pcd.Cisterna}");
            var datosEvento = new EventoRequestDto
            {
                TipoEvento = "DESPACHO",
                Descripcion = "prueba de descripcion",
            };

            var idEvento = await _IEventoService.CrearEvento(datosEvento);
            // =============================================
            // MOSTRAR CISTERNAS
            // =============================================
            foreach (var item in pcd.Cisterna)
            {
                if (item == null) continue;

                var id_cisterna = item.Id;

                // OBTENGO LOS DATOS DEL CONDUCTOR, LA PLACA, VOLUMEN TM
                // =============================================
                var Cist = await _context.TbCisternaDetalles.FirstOrDefaultAsync(x => x.Id == item.Id);

                Instancia = new TbInstancium
                {
                    IdTipoLugar = 2,
                    Codigo = Cist.Placa,
                    Nombre = "Cisterna " + Cist.Placa,
                    Estado = true,
                    Activo= true,
                    CreadoEn = DateTime.Now,
                    IdCreadoPor = usuarioAutenticado.IdUsuario,
                    CreadoPor = usuarioAutenticado.Email.Split("@")[0].ToUpper()
                };
                _context.TbInstancia.Add(Instancia);

                await _context.SaveChangesAsync();

                var SaveCist = new TbCisterna
                {
                    IdInstancia = Instancia.IdInstancia,
                    IdCisternaDetalle = item.Id,
                    Estado = true,
                    Activo= true,
                    CreadoEn = DateTime.Now,
                    IdCreadoPor = usuarioAutenticado.IdUsuario,
                    CreadoPor = usuarioAutenticado.Email.Split("@")[0].ToUpper()
                };
                _context.TbCisternas.Add(SaveCist);
                await _context.SaveChangesAsync();
                y += 1;
                var eventosDto = new EventoRequestDto
                {
                    IdPlanta = pcd.IdPlanta,
                    IdInstancia = Instancia.IdInstancia,
                    TipoAccion = "ORIGEN",
                    TipoEvento = "DESPACHO",
                    Descripcion = "prueba de descripcion",
                    Data = Cist.Id,
                    EtapaFlujo = "CISTERNA",
                    CantEvento=y,
                    IdEvento= idEvento
                };
                await _IEventoService.AlmacenarEvento(eventosDto);

            }
            await transaccion.CommitAsync();
            

            System.Diagnostics.Debug.WriteLine("FIN CrearCisternasAsync");
            System.Diagnostics.Debug.WriteLine("=====================================");

            return new CrearCisternasDto
            {
                IdInstancia = planta.IdInstancia,
                IdCisternaDetalle = planta.IdPlanta
            };
        }

        public async Task<CrearOperadorDto> AdicionarOperadorAsync(CrearOperadorDto coi)
        {
            ArgumentNullException.ThrowIfNull(coi);
            _logger.LogInformation("======  Datos recibidos en AlmacenarEvento: {Evento}", JsonSerializer.Serialize(coi, new JsonSerializerOptions
            {
                WriteIndented = true
            }));

            try
            {
                var usuario = ObtenerIdUsuario();

                await using var transaccion = await _context.Database.BeginTransactionAsync();
                // ================================================================================
                // 1. BUSCAR OPERADOR
                // ================================================================================
                var operador = await _context.TbInstancia.FirstOrDefaultAsync(x => x.Codigo == coi.CodOperador);
                // ================================================================================
                // 2. CREAR NUEVA INSTANCIA
                // ================================================================================
                var instancia = new TbInstancium
                {
                    Codigo = coi.CodOperador,
                    Nombre = coi.Descripcion,
                    IdTipoLugar = 1,
                    Ubicacion = coi.PaisImpor,
                    Estado = true,
                    Activo = true, 
                    CreadoEn=DateTime.Now,
                    IdCreadoPor= usuario.IdUsuario,
                    CreadoPor = usuario.Email.Split("@")[0].ToUpper()
                };
                _context.TbInstancia.Add(instancia);
                await _context.SaveChangesAsync();
                // ================================================================================
                // 3. CREAR NUEVA PLANTA
                // ================================================================================
                var plant = new TbPlantum
                {
                    IdInstancia = instancia.IdInstancia,
                    TipoOperacion = 2,
                    Pais = coi.PaisImpor,
                    Departamento = "",
                    PuntoIngreso = coi.PuntoIngreso,
                    Estado = true,
                    Observacion = coi.ObsOperador,
                    Activo = true,
                    CreadoEn = DateTime.Now,
                    IdCreadoPor = usuario.IdUsuario,
                    CreadoPor = usuario.Email.Split("@")[0].ToUpper()
                };
                _context.TbPlanta.Add(plant);
                await _context.SaveChangesAsync();

                await transaccion.CommitAsync();
                // ================================================================================
                // 4. RETORNA RESULTADO
                // ================================================================================
                return coi;
            }
            catch (DbUpdateException ex)
                when (ex.InnerException is PostgresException pgEx &&
                      pgEx.SqlState == PostgresErrorCodes.UniqueViolation &&
                      pgEx.ConstraintName == "uq_instancia_tipo_codigo")
            {
                throw new InvalidOperationException(
                    $"La planta con código '{coi.CodOperador}' ya se encuentra registrada."
                );
            }
        }

        public async Task<List<TbPlantum>> ObtenerOperadorAsync() {
            var resQuery = await (
                    from tp in _context.TbPlanta
                    where tp.TipoOperacion == 2
                    select new TbPlantum
                    {
                        TipoOperacion = tp.TipoOperacion,
                        Pais = tp.Pais
                    }
                ).AsNoTracking().ToListAsync();
            System.Diagnostics.Debug.WriteLine($" ===============ObtenerOperador = > {resQuery}");
            return resQuery;
        }
        private static DateTime SinZonaHoraria(DateTime fecha)
        {
            return DateTime.SpecifyKind(
                fecha,
                DateTimeKind.Unspecified
            );
        }

        private string GenerarCodigoTrazabilidad()
        {
            return $"TRZ-{DateTime.Now:yyyyMMdd}-{Guid.NewGuid()
                .ToString("N")[..6]
                .ToUpper()}";
        }

        private (long IdUsuario, string Email) ObtenerIdUsuario()
        {
            var user = _httpContextAccessor.HttpContext?.User;

            var IdUsuarioClaim = user?.FindFirstValue(ClaimTypes.NameIdentifier);

            var emailClain = user?.FindFirstValue(ClaimTypes.Email);

            if (string.IsNullOrWhiteSpace(IdUsuarioClaim)) throw new UnauthorizedAccessException("No se pudo obtener el ID del usuario autenticado.");

            if (!long.TryParse(IdUsuarioClaim, out var idUsuario)) throw new UnauthorizedAccessException("El ID del usuario no es válido.");

            if (string.IsNullOrWhiteSpace(emailClain)) throw new UnauthorizedAccessException("No se pudo obtener el Correo del usuario autenticado.");


            return (idUsuario, emailClain);
        }
    }
}
