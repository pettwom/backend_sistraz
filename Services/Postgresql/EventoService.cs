using backend_trazabilidad.DTOs.Postgresql;
using backend_trazabilidad.Models.Postgresql;
using DocumentFormat.OpenXml.Office.Word;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.Security.Claims;
using System.Text.Json;

namespace backend_trazabilidad.Services.Postgresql
{
    public class EventoService : IEventoService
    {
        private readonly PostgresDbContext _context;
        private readonly ILogger<ProduccionService> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public EventoService(PostgresDbContext context, ILogger<ProduccionService> logger, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<long> CrearEvento(EventoRequestDto evr)
        {
            ArgumentNullException.ThrowIfNull(evr);
            _logger.LogInformation("======  Datos recibidos en AlmacenarEvento: {Evento}", JsonSerializer.Serialize(evr, new JsonSerializerOptions
            {
                WriteIndented = true
            }));

            var usuarioAutenticado = ObtenerIdUsuario();
            try
            {
                var evento = new TbEvento
                {
                    TipoEvento = evr.TipoEvento,
                    FechaEvento = DateTime.Now,
                    Descripcion = evr.Descripcion,
                    Estado = "ACTIVO",
                    Activo = true,
                    CreadoEn = DateTime.Now,
                    IdCreadoPor = usuarioAutenticado.IdUsuario,
                    CreadoPor = usuarioAutenticado.Email.Split("@")[0].ToUpper()
                };
                _context.TbEventos.Add(evento);
                await _context.SaveChangesAsync();
                return evento.IdEvento;
            }
            catch (Exception ex)
            {
                throw new Exception($"No se creo el evento");
            }
        }
        public async Task<EventoRequestDto> AlmacenarEvento(EventoRequestDto evr)
        {

            ArgumentNullException.ThrowIfNull(evr);

            _logger.LogInformation(
                "======  Datos recibidos en AlmacenarEvento: {Evento}",
                JsonSerializer.Serialize(evr, new JsonSerializerOptions
                {
                    WriteIndented = true
                }));


            var usuarioAutenticado = ObtenerIdUsuario();

            try
            {
                //==========================================================================
                //RECUPERAR DATOS PARA EL REGISTRO EN EL EVENTO 
                //==========================================================================
                //var planta = await _context.TbPlanta.FirstOrDefaultAsync(x => x.IdInstancia == evr.IdInstancia);//obtenemos id_planta
                //if (planta is null) throw new Exception($"No existe la Instancia con id_instancia = {evr.IdInstancia}");

                var lote = await _context.TbLoteGlps.FirstOrDefaultAsync(x => x.IdPlantaOrigen == evr.IdPlanta);//obtenemos id_lote
                if (lote is null) throw new Exception($"No Existe el lote con la instancia {evr.IdInstancia}");

                //==========================================================================
                //ALMACENA EL EVENTO 
                //==========================================================================

                if (evr.TipoAccion == "ORIGEN")
                {
                    decimal? dataRes = null;
                    switch (evr.EtapaFlujo)
                    {
                        case "CISTERNA":
                            var data = await _context.TbCisternaDetalles.FirstOrDefaultAsync(x => x.Id == evr.Data);
                            if (data is null) throw new Exception($"No se encontraron datos de Cisternas con el id: {data}");
                            dataRes = data.VolBbls.Value;
                            var events = await _context.TbEventoOrigens.FirstOrDefaultAsync(x => x.IdInstancia == evr.IdInstancia && x.IdLote == lote.IdLote);
                            if (events is null) throw new Exception($"No se registro ningun evento");
                            break;
                    }
                    var evento_origen = new TbEventoOrigen
                    {
                        IdEvento = evr.IdEvento,
                        IdInstancia = evr.IdInstancia,
                        IdLote = lote.IdLote,
                        Volumen = (decimal)dataRes,
                        Activo = true,
                        CreadoEn = DateTime.Now,
                        IdCreadoPor = usuarioAutenticado.IdUsuario,
                        CreadoPor = usuarioAutenticado.Email.Split("@")[0].ToUpper()
                    };
                    _context.TbEventoOrigens.Add(evento_origen);
                    await _context.SaveChangesAsync();

                }
                if (evr.TipoAccion == "DESTINO")
                {

                    decimal? dataRes = null;
                    switch (evr.EtapaFlujo)
                    {
                        case "CISTERNA":
                            var data = await _context.TbCisternaDetalles.FirstOrDefaultAsync(x => x.Id == evr.Data);
                            System.Diagnostics.Debug.WriteLine($"EL DATO DE LA CISTERNA ES {data}");
                            if (data is null) throw new Exception($"No se encontraron datos de Cisternas con el id: {evr.Data}");

                            dataRes = data.VolBbls.Value;

                            break;
                    }
                    var evento_destino = new TbEventoDestino
                    {
                        IdEvento = evento.IdEvento,
                        IdInstancia = evr.IdInstancia,
                        IdLote = lote.IdLote,
                        Volumen = (decimal)dataRes,
                        Activo = true,
                        CreadoEn = DateTime.Now,
                        IdCreadoPor = usuarioAutenticado.IdUsuario,
                        CreadoPor = usuarioAutenticado.Email.Split("@")[0].ToUpper()
                    };
                    _context.TbEventoDestinos.Add(evento_destino);

                }
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            //await transaccion.CommitAsync();
            return evr;
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
