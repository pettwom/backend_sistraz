using backend_trazabilidad.DTOs.Octano;
using System.Globalization;
using System.Net.Http.Json;


namespace backend_trazabilidad.Services.Octano
{
    public class CertificadoOctanoService
    {
        private readonly HttpClient _http;
        private readonly IConfiguration _configuration;

        public CertificadoOctanoService(
            HttpClient http,
            IConfiguration configuration)
        {
            _http = http;
            _configuration = configuration;
        }

        public async Task<List<CertificadoAlertaOctanoDto>> ListarAsync(
            DateTime desde,
            DateTime hasta,
            decimal? idEntidad,
            decimal idUsuario,
            bool esSuperAdministrador,
            CancellationToken cancellationToken = default)
        {
            var credencial = _configuration["Octano:Credencial"]
                ?? throw new InvalidOperationException("Falta Octano:Credencial");

            // Misma lógica que tenía el código con Session.
            decimal entidadOctano = idEntidad.GetValueOrDefault();
            decimal usuarioOctano =
                idEntidad is null && esSuperAdministrador ? 0 : idUsuario;

            long fechaInicial = long.Parse(
                desde.Date.ToString("yyyyMMddHHmmss", CultureInfo.InvariantCulture));

            long fechaFinal = long.Parse(
                hasta.Date.AddDays(1).AddSeconds(-1)
                    .ToString("yyyyMMddHHmmss", CultureInfo.InvariantCulture));

            var ruta =
                $"ReportarAlertaCertificado/{Uri.EscapeDataString(credencial)}" +
                $"/{entidadOctano}/{usuarioOctano}" +
                $"/{fechaInicial}/{fechaFinal}?format=json";

            return await _http.GetFromJsonAsync<List<CertificadoAlertaOctanoDto>>(
                ruta, cancellationToken) ?? [];
        }
    }
}
