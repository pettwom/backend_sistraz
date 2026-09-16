using backend_trazabilidad.Services.Postgresql;
using Microsoft.AspNetCore.Mvc;

namespace backend_trazabilidad.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CatalogoController : ControllerBase
    {
        private readonly ICatalogoService _service;
        public CatalogoController(ICatalogoService service)
        {
            _service = service;
        }
        [HttpGet("params")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _service.ObtenerParametricasAsync());
        }
    }
}
