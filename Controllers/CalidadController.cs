
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

        private readonly string I_CREDENCIAL = "83809AD945F1F72D0EA9FDA0E599E0B3";

        [HttpGet("parametros")]
        public async Task<IActionResult> ObtenerParametros()
        {
            System.Diagnostics.Debug.WriteLine("===================================================================");
            System.Diagnostics.Debug.WriteLine("===================================================================");
            decimal idTabla = 1;

            decimal idEntidad = 36252;
            DateTime fecha =
                        new DateTime(
                            2015,
                            8,
                            17,
                            15,
                            56,
                            34
                        );
            string cite = "0";
            var resultado =
                await _service.ObtenerParametrosAsync(
                    credencial: I_CREDENCIAL,
                    //credencial: "eyJhbGciOiJIUzI1NiJ9.eyJVc2VybmFtZSI6IlJFREVTIEDBUyJ9.JI2CdBI3HSV0Awg_rWcmnKvg_GRmnHUTXx3jpgGlG14",
                    idTablaEspecificacion: idTabla,
                    idEntidad: idEntidad,
                    fecha: fecha,
                    cite: cite
                );
            return Ok(new
            {
                idTabla,
                idEntidad,
                fecha,
                cite,
                cantidad = resultado.Count,
                datos = resultado
            });
        }

        [HttpGet("reporte")]
        public async Task<IActionResult> ObtenerReporte()
        {
            var cite = "ANH-REB-DO 0011";
            var resultado = await _service.ObtenerReporteCalidadAsync(I_CREDENCIAL, cite);
            return Ok(resultado);
        }
    }
}