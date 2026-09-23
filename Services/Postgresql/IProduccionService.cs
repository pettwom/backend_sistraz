using backend_trazabilidad.DTOs.Postgresql;
using DocumentFormat.OpenXml.Office.Word;

namespace backend_trazabilidad.Services.Postgresql
{
    public interface IProduccionService
    {
        Task<List<ProduccionDto>> ObtenerListadoAsync();
        Task<CrearProduccionRequestDto> CrearProdAsync(CrearProduccionRequestDto dto);
        Task<CrearPlantaDto> CrearPlantaAsync(CrearPlantaDto plt);
        Task<ProdCisternaDto> CrearCisternasAsync(ProdCisternaDto pcd);
    }
}
