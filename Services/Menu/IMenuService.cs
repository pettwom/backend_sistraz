using backend_trazabilidad.DTOs.Oracle;

namespace backend_trazabilidad.Services.Menu
{
    public interface IMenuService
    {
        Task<List<MenuDto>>ObtenerMenusAsync(decimal idUsuario, decimal idMdolulo);
    }
}
