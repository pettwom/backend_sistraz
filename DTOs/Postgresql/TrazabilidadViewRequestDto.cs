using System.Text.Json.Serialization;

namespace backend_trazabilidad.DTOs.Postgresql
{
    public class TrazabilidadViewRequestDto
    {
        [JsonPropertyName("codigoTrazabilidad")]
        public string codigoTrazabilidad { get; set; } = string.Empty;
    }
}
