using backend_trazabilidad.Services.Menu;
using Microsoft.AspNetCore.Mvc;

namespace backend_trazabilidad.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenuController:ControllerBase
    {
        private readonly IMenuService _menuService;
        public MenuController(IMenuService menuService)
        { 
            _menuService = menuService;
        }

        [HttpGet("{idUsuario}/{idModulo}")]
        public async Task<IActionResult> ObtenerMenus(decimal idUsuario, decimal idModulo)
        { 
            var menu = await _menuService.ObtenerMenusAsync(idUsuario, idModulo);
            return Ok(menu); 
        }
    }
}
