using backend_trazabilidad.DTOs.Postgresql;

namespace backend_trazabilidad.Services.Postgresql
{
    public interface IProduccionService
    {
        Task<List<ProduccionDto>> ObtenerListadoAsync();
    }
}
