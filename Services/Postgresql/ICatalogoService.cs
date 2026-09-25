using backend_trazabilidad.DTOs.Postgresql;

namespace backend_trazabilidad.Services.Postgresql
{
    public interface ICatalogoService
    {
        Task<List<SelectOptionDto>> ObtenerParametricasAsync();
        Task<List<SelectOptionDto>> ObtenerPaisAsync();
    }
}
