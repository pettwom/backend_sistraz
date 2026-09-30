
//using backend_trazabilidad.Services.Octano;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using System.Globalization;
//using System.Security.Claims;


//namespace backend_trazabilidad.Controllers
//{
//    [Authorize]
//    [ApiController]
//    [Route("api/[controller]")]
//    public class CalidadController : ControllerBase
//    {
//        private readonly CertificadoOctanoService _certificados;
//        private readonly string I_CREDENCIAL = "83809AD945F1F72D0EA9FDA0E599E0B3";
//        public CalidadController(CertificadoOctanoService certificados)
//        {
//            _certificados = certificados;
//        }

//        [HttpGet("certificados-alertas")]
//        public async Task<IActionResult> ObtenerCertificadosAlertas(
//    [FromQuery] DateTime desde,
//    [FromQuery] DateTime hasta,
//    CancellationToken cancellationToken)
//        {
//            if (hasta.Date < desde.Date)
//                return BadRequest("La fecha final no puede ser anterior a la inicial.");
//            var idUsuarioTexto =
//                User.FindFirstValue("idUsuario")
//                ?? User.FindFirstValue(ClaimTypes.NameIdentifier);
//            //idUsuarioTexto = User.FindFirstValue("idUsuario");
//            idUsuarioTexto = User.FindFirstValue(ClaimTypes.NameIdentifier);
//            if (!decimal.TryParse(
//                    "5404",
//                    NumberStyles.Number,
//                    CultureInfo.InvariantCulture,
//                    out var idUsuario))
//            {
//                return Unauthorized("No se encontró un idUsuario válido en el token.");
//            }

//            var entidadTexto = User.FindFirstValue("entidad");

//            decimal? idEntidad = decimal.TryParse(
//                entidadTexto,
//                NumberStyles.Number,
//                CultureInfo.InvariantCulture,
//                out var entidad)
//                    ? entidad
//                    : null;

//            bool esSuperAdministrador = User.IsInRole("SuperAdministrador");

//            var resultado = await _certificados.ListarAsync(
//                desde,
//                hasta,
//                0,
//                0,
//                esSuperAdministrador,
//                cancellationToken);

//            return Ok(resultado);
//        }
//    }
//    }

using backend_trazabilidad.DTOs.Octano;
using backend_trazabilidad.Services.Octano;
using backend_trazabilidad.Services.Postgresql;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Net.Http.Json;
using System.Security.Claims;

namespace backend_trazabilidad.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CalidadController : ControllerBase
{
    private readonly CalidadOctanoService _service;
    private readonly IConfiguration _configuration;

    private readonly ILogger<ProduccionService> _logger;

    public CalidadController(CalidadOctanoService service, IConfiguration configuration, ILogger<ProduccionService> logger)
    {
        _service = service;
        _configuration = configuration;
        _logger = logger;
    }

    private string Credencial =>
        _configuration["Octano:Credencial"]
        ?? throw new InvalidOperationException("Falta Octano:Credencial.");

    [Authorize]
    [HttpGet("certificados")]
    public async Task<IActionResult> ListarCertificados([FromQuery] DateTime desde,[FromQuery] DateTime hasta,[FromQuery] int Entidad,CancellationToken cancellationToken)
    {

        if (hasta.Date < desde.Date)
            return BadRequest("La fecha final no puede ser anterior a la inicial.");

        var usuarioTexto =
            User.FindFirstValue("idUsuarioHydro")
            ?? User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!decimal.TryParse(usuarioTexto, out var idUsuario))
            return Unauthorized("Falta el ID de usuario Hydro en el token.");

        decimal.TryParse(User.FindFirstValue("idEntidad"), out var idEntidad);
        

        var datos = await _service.ListarCertificadosPrincipalAsync(
            Credencial,
            desde,
            hasta,
            Entidad,
            idUsuario,
            User.IsInRole("SuperAdministrador"),
            cancellationToken);

        return Ok(datos);
    }


    [Authorize]
    [HttpGet("parametros")]
    public async Task<IActionResult> ObtenerParametros([FromQuery] decimal idTabla, [FromQuery] decimal idEntidad, [FromQuery] DateTime fecha, [FromQuery] string cite = "0")
    {
        var datos = await _service.ObtenerParametrosAsync(Credencial, idTabla, idEntidad, fecha, cite);

        return Ok(datos);
    }

    [Authorize]
    [HttpGet("reporte")]
    public async Task<IActionResult> ObtenerReporte([FromQuery] string cite)
    {
        if (string.IsNullOrWhiteSpace(cite))
            return BadRequest("Debe indicar el CITE.");

        var datos = await _service.ObtenerReporteCalidadAsync(
            Credencial, "ANH00006 - IMPCAR01 - GE0003_2026");

        return Ok(datos);
    }

    [Authorize]
    [HttpGet("certificados-alertas")]
    public async Task<IActionResult> ObtenerCertificadosAlertas([FromQuery] DateTime desde, [FromQuery] DateTime hasta, CancellationToken cancellationToken)
    {
        if (hasta.Date < desde.Date)
            return BadRequest("La fecha final no puede ser anterior a la inicial.");

        // ID_USUARIO de Hydro; debe estar incluido en el JWT.
        var usuarioTexto =
            User.FindFirstValue("idUsuarioHydro")
            ?? User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!decimal.TryParse(
                usuarioTexto,
                NumberStyles.Integer,
                CultureInfo.InvariantCulture,
                out var idUsuario))
        {
            return Unauthorized(
                "El token no contiene el ID de usuario Hydro.");
        }

        var entidadTexto = User.FindFirstValue("idEntidad");

        if (!decimal.TryParse(
                entidadTexto,
                NumberStyles.Integer,
                CultureInfo.InvariantCulture,
                out var idEntidad))
        {
            return Unauthorized(
                "El token no contiene el ID de entidad Hydro.");
        }

        var esSuperAdministrador = User.IsInRole("SuperAdministrador");

        var datos = await _service.ListarCertificadosAlertaAsync(
            Credencial,
            desde,
            hasta,
            idEntidad,
            idUsuario,
            esSuperAdministrador,
            cancellationToken);

        return Ok(datos);
    }
}