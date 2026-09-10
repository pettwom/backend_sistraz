using backend_trazabilidad.Models.Hydro;

namespace backend_trazabilidad.Services.Hydro
{
    public interface IHydroSoapService
    {
        Task<HydroAuthResult?>AutenticarAsync(string usuario, string password);
        Task<List<string>> ObtenerPerfilesAsync(decimal idUsuario);
    }
}
