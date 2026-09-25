using System.Text.Json.Serialization;

namespace backend_trazabilidad.DTOs.Postgresql
{
    public class CrearOperadorDto
    {
        [JsonPropertyName("cod_operador")]
        public string CodOperador { get; set; } = string.Empty;
        [JsonPropertyName("desc_operador")]
        public string Descripcion { get; set; } = string.Empty;
        [JsonPropertyName("paisImpor")]
        public string PaisImpor { get; set; } = string.Empty;
        [JsonPropertyName("punto_ingreso")]
        public string PuntoIngreso { get; set; } = string.Empty;
        [JsonPropertyName("obs_operador")]
        public string? ObsOperador { get; set; } = string.Empty;
    }
}
