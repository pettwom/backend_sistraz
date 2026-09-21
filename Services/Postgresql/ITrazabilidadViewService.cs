using backend_trazabilidad.DTOs.Postgresql;

namespace backend_trazabilidad.Services.Postgresql
{
    public interface ITrazabilidadViewService
    {
        Task<List<TrazabilidadViewResponseDto>> ObtenerTrazabilidadAsync(TrazabilidadViewRequestDto tvr);
    }
}
