using backend_trazabilidad.DTOs.Oracle;
using Microsoft.EntityFrameworkCore;

namespace backend_trazabilidad.Services.Menu
{
    public class MenuService : IMenuService
    {
        private readonly AplicationDbContext _context;
        public MenuService(AplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<MenuDto>> ObtenerMenusAsync(decimal idUsuario, decimal idModulo)
        {
            
            var menu = await (
                from tu in _context.Usuario

                join tpu in _context.PerfilUsuario
                    on tu.IdUsuario equals tpu.IdUsuario

                join tp in _context.Perfiles
                    on tpu.IdPerfil equals tp.IdPerfil

                join tmp in _context.MenuesPerfil
                    on tp.IdPerfil equals tmp.IdPerfil

                join tm in _context.Menues
                    on tmp.IdMenu equals tm.IdMenu

                where tu.IdUsuario == idUsuario
                    && tm.IdModulo == 122

                orderby tm.Orden ascending

                select new MenuDto
                {
                    IdMenu = tm.IdMenu,
                    Titulo = tm.Titulo,
                    Enlace = tm.Enlace,
                    IdMenuPadre = tm.IdMenuPadre,
                    Icono = null,
                    Orden = tm.Orden,
                    Descripcion = tm.Descripcion
                }
                ).AsNoTracking().ToListAsync();
            return menu;
        }
    }
}
