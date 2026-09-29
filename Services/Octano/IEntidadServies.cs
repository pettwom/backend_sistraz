using backend_trazabilidad.DTOs.Oracle;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace backend_trazabilidad.Services.Octano
{
    public interface IEntidadServies
    {
        Task<List<EntidadDto>> ObtenerListadoEntidadLocalAsync();
        Task<List<EntidadDto>> ObtenerListadoEntidadImportacionAsync();
    }
}
