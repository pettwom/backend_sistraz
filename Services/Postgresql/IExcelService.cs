using backend_trazabilidad.DTOs.Postgresql;

namespace backend_trazabilidad.Services.Postgresql
{
    public interface IExcelService
    {
        Task<List<CisternaExcelDto>> ObtenerListadoAsync();
        Task<List<CisternaExcelDto>> ObtenerListSelectAsync();
    }
}
