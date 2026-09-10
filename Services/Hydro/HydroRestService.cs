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
                var baseUrl = _configuration["Hydro:Url"];
                var credencial = _configuration["Hydro:CredencialRrhh"];

                if (string.IsNullOrWhiteSpace(baseUrl))
                    throw new Exception("No existe Hydro:Url en appsettings.json");

                if (string.IsNullOrWhiteSpace(credencial))
                    throw new Exception("No existe Hydro:CredencialRrhh en appsettings.json");

                baseUrl = baseUrl.TrimEnd('/');

                var url =
                    $"{baseUrl}/WSRecursosHumanos/EDatosFuncionarioANH/" +
                    $"{Uri.EscapeDataString(credencial)}/{idUsuario}";

                Console.WriteLine("====================================");
                Console.WriteLine($"BASE URL   : {baseUrl}");
                Console.WriteLine($"ID USUARIO : {idUsuario}");
                Console.WriteLine($"URL FINAL  : {url}");
                Console.WriteLine("====================================");

                var response = await _httpClient.GetAsync(url);

                Console.WriteLine(
                    $"STATUS HYDRO: {(int)response.StatusCode} - {response.StatusCode}"
                );

                var contenido =
                    await response.Content.ReadAsStringAsync();

                Console.WriteLine($"RESPUESTA HYDRO: {contenido}");

                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                return await response.Content
                    .ReadFromJsonAsync<FuncionarioResponse>();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR HYDRO:");
                Console.WriteLine(ex.ToString());

                throw;
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