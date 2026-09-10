using System.Text.Json.Serialization;

namespace backend_trazabilidad.Models.Hydro
{
    public class FuncionarioDto
    {
        [JsonPropertyName("estado")]
        public string Estado { get; set; }
        [JsonPropertyName("fotO_DIGITAL_ID")]
        public decimal? FotoDigitalId { get; set; }
        [JsonPropertyName("iD_ORGANIGRAMA")]
        public decimal? IdOrganigrama { get; set; }
        [JsonPropertyName("direccion")]
        public string? Direccion { get; set; }

        [JsonPropertyName("denominacioN_UNIDAD")]
        public string? DenominacionUnidad { get; set; }

        [JsonPropertyName("cargo")]
        public string? Cargo { get; set; }

        [JsonPropertyName("nuM_IDENTIDAD")]
        public string? NumIdentidad { get; set; }

        [JsonPropertyName("usuariO_HYDRO_ID")]
        public decimal? UsuarioHydroId { get; set; }
    }
}
