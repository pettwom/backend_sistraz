using backend_trazabilidad.Models.Hydro;

namespace backend_trazabilidad.Services.Hydro
{
    public class HydroRestService : IHydroRestService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger<HydroRestService> _logger;

        public HydroRestService(HttpClient httpClient, IConfiguration configuration, ILogger<HydroRestService> logger)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _logger = logger;
        }

        private string HydroUrl => _configuration["Hydro:url"] ?? throw new Exception("Hydro:Url no configurado.");

        private string Credencial => _configuration["Hydro:credencial"] ?? throw new Exception("Hydro:credencial no configurado.");

        public async Task<FuncionarioResponse?> ObtenerFuncionarioAsync(decimal idUsuario)
        {
            try
            {
                var url = $"{HydroUrl}" +
                    $"WSRecursosHumanos/" +
                    $"EDatosFuncionriosANH/" +
                    $"{Credencial}/" +
                    $"{idUsuario}?format=json";

                _logger.LogInformation("Consultado Funcio HYDRO {IdUsuario}", idUsuario);

                var resultado = await _httpClient.GetFromJsonAsync<FuncionarioResponse>(url);

                return resultado;
            }

            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error consultando funcionario HYDRO."
                );
                throw;
                //var url = 
                //    $"{HydroUrl}"+
                //    $"WSRecursosHumanos/" +
                //    $"ListaFuncionariosActivos/" +
                //    $"{Credencial}?format=json"; 

                //return await _httpClient.GetFromJsonAsync<ListaFuncionariosResponse>(url);

                //_logger.LogError(ex, "Excepción al obtener funcionario.");
                //return null;
            }
        }

        public async Task<ListaFuncionariosResponse?> ObtenerFuncionariosActivosAsync()
        {
            try
            {
                var url = $"{HydroUrl}" +
                    $"WSRecursosHumanos/" +
                    $"ListaFuncionariosActivos/" +
                    $"{Credencial}?format=json";

                return await _httpClient.GetFromJsonAsync<ListaFuncionariosResponse>(url);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error consultando lista de funcionarios activos HYDRO.");
                throw;
            }
        }
        public async Task<string?> ObtenerFotosAsync(decimal idFoto)
        {
            try
            {
                var url = $"{HydroUrl}" +
                    $"WSRecursosHumanos/" +
                    $"EFotoPerfilFuncionario/" +
                    $"{Credencial}/" +
                    $"{idFoto}?format=json";

                var foto = await _httpClient.GetFromJsonAsync<FotoResponse>(url);
                if (string.IsNullOrWhiteSpace(foto?.oResultado?.ArchivoBinario))
                {
                    return null;
                }
                return $"data:image/png;base64,{foto.oResultado.ArchivoBinario}";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error consultando foto HYDRO.");
                throw;
            }
        }

    }
}