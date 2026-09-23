using backend_trazabilidad.DTOs.Octano;
namespace backend_trazabilidad.Services.Octano
{
    public interface ICalidadOctanoService
    {
        Task<List<CalidadOctanoDto>>ObtenerParametrosGlpAsync(DateTime fecha);
    }
}