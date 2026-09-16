using backend_trazabilidad.DTOs.Postgresql;
using backend_trazabilidad.Services.Postgresql;
using Microsoft.AspNetCore.Mvc;

namespace backend_trazabilidad.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdController:ControllerBase
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
        [HttpPost("addPlanta")]
        public async Task<IActionResult> AddPlanta([FromBody] CrearPlantaDto dto)
        {
            var resultado = await _produccionService.CrearPlantaAsync(dto);
            return Ok(resultado);
        }
    }
}
