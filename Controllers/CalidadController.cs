
using backend_trazabilidad.Services.Octano;
using Microsoft.AspNetCore.Mvc;

namespace backend_trazabilidad.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CalidadController : ControllerBase
    {
        private readonly CalidadOctanoService _service;

        public CalidadController(
            CalidadOctanoService service)
        {
            _service = service;
        }

        [HttpGet("parametros")]
        public async Task<IActionResult> ObtenerParametros(
            [FromQuery] decimal idTabla,
            [FromQuery] decimal idEntidad)
        {
            System.Diagnostics.Debug.WriteLine("===================================================================");
            System.Diagnostics.Debug.WriteLine("1. Esto es el id IdTabla: =>>" + idTabla);
            System.Diagnostics.Debug.WriteLine("2. Esto es el id IdEntidad: =>>" + idEntidad);
            System.Diagnostics.Debug.WriteLine("===================================================================");
            var resultado =
                await _service.ObtenerParametrosAsync(
                    credencial: "eyJhbGciOiJIUzI1NiJ9.eyJVc2VybmFtZSI6IlJFREVTIEDBUyJ9.JI2CdBI3HSV0Awg_rWcmnKvg_GRmnHUTXx3jpgGlG14",
                    idTablaEspecificacion: 54,
                    idEntidad: idEntidad,
                    fecha: DateTime.Now,
                    cite: "0"
                );

            return Ok(resultado);
        }
    }
}