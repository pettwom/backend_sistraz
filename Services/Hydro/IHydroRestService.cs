using backend_trazabilidad.Models.Hydro;

namespace backend_trazabilidad.Services.Hydro
{
    public interface IHydroRestService
    {
        Task<FuncionarioResponse?> ObtenerFuncionarioAsync(decimal idUsuario);

        Task<ListaFuncionariosResponse?> ObtenerFuncionariosActivosAsync();

        Task<string?> ObtenerFotosAsync(decimal idFoto);
    }
}
