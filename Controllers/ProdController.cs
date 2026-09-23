using backend_trazabilidad.DTOs.Postgresql;
using backend_trazabilidad.Services.Postgresql;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace backend_trazabilidad.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProdController : ControllerBase
    {
        private readonly IProduccionService _produccionService;
        public ProdController(IProduccionService produccionService)
        {
            _produccionService = produccionService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var prod = await _produccionService.ObtenerListadoAsync();
            return Ok(prod);
        }
        [Authorize]
        [HttpPost("addPlanta")]
        public async Task<IActionResult> AddPlanta([FromBody] CrearPlantaDto dto)
        {
            try
            {
                var resultado = await _produccionService.CrearPlantaAsync(dto);
                return Ok(new
                {
                    exito = true,
                    mensaje = "Planta registrada correctamente.",
                    data = resultado
                });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new
                {
                    exito = false,
                    mensaje = ex.Message
                });
            }
            catch (Exception)
            {
                return StatusCode(500, new
                {
                    exito = false,
                    mensaje = "Ocurrió un error al registrar la planta."
                });
            }


        }
        [Authorize]
        [HttpPost("addProd")]
        public async Task<IActionResult> CrearProd([FromBody] CrearProduccionRequestDto dto)
        {
            try
            {
                var resultado = await _produccionService.CrearProdAsync(dto);
                return Ok(new
                {
                    exito = true,
                    mensaje = "Lote registrada correctamente.",
                    data = resultado
                });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new
                {
                    exito = false,
                    mensaje = ex.Message
                });
            }
            catch (Exception)
            {
                return StatusCode(500, new
                {
                    exito = false,
                    mensaje = "Ocurrió un error al registrar la planta."
                });
            }
        }
        [Authorize]
        [HttpGet("addDespachar")]
        public async Task<IActionResult> CrearCisterna([FromBody] ProdCisternaDto pcd) 
        {
            var resultado = await _produccionService.CrearCisternasAsync(pcd);
            return Ok(resultado);
        }
    }
}
