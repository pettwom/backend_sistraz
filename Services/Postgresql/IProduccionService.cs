using backend_trazabilidad.DTOs.Postgresql;
using backend_trazabilidad.Models.Postgresql;
using DocumentFormat.OpenXml.Office.Word;

namespace backend_trazabilidad.Services.Postgresql
{
    public interface IProduccionService
    {
        Task<List<ProduccionDto>> ObtenerListadoAsync();
        Task<CrearProduccionRequestDto> CrearProdAsync(CrearProduccionRequestDto dto);
        Task<CrearPlantaDto> CrearPlantaAsync(CrearPlantaDto plt);
        Task<CrearCisternasDto> CrearCisternasAsync(ProdCisternaDto pcd);
        Task<CrearOperadorDto> AdicionarOperadorAsync(CrearOperadorDto coi);
        Task<List<CargaOperadorDto>> ObtenerOperadorAsync();
    }
}
