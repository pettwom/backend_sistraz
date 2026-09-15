using backend_trazabilidad.DTOs.Postgresql;

namespace backend_trazabilidad.Services.Postgresql.Produccion
{
    public interface IProduccionService
    {
        Task<List<ProduccionDto>> ObtenerProduccionAsync();
    }
}
