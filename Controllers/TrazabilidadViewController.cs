using Microsoft.AspNetCore.Mvc;
using backend_trazabilidad.DTOs.Postgresql;
using backend_trazabilidad.Services.Postgresql;
using Microsoft.AspNetCore.Authorization;

namespace backend_trazabilidad.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TrazabilidadViewController:ControllerBase
    {
        private readonly ITrazabilidadViewService _context;
        public TrazabilidadViewController(ITrazabilidadViewService context)
        {
            _context = context;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Resultado([FromQuery] TrazabilidadViewRequestDto tvr)
        {
            var res = await _context.ObtenerTrazabilidadAsync(tvr);
            return Ok(res); 
        }
     }
}
