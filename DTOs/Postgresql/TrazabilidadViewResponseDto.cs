using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace backend_trazabilidad.DTOs.Postgresql
{
    public class TrazabilidadViewResponseDto
    {
        [JsonPropertyName("IdLote")]
        public long? IdLote { get; set; }
        [JsonPropertyName("CodigoTrazabilidad")]
        public string? CodigoTrazabilidad { get; set; } = string.Empty;
        [JsonPropertyName("IdEvento")]
        public long? IdEvento { get; set; }
        [JsonPropertyName("TipoEvento")]
        public string? TipoEvento { get; set; } = string.Empty;
        [JsonPropertyName("FechaEvento")]
        public DateTime? FechaEvento { get; set; }
        [JsonPropertyName("Estado")]
        public string? Estado { get; set; } = string.Empty;
        [JsonPropertyName("Origenes")]
        public JsonArray? Origenes { get; set; } = new();
        [JsonPropertyName("Destinos")]
        public JsonArray? Destinos { get; set; } = new();
        [JsonPropertyName("Certificados")]
        public JsonArray? Certificados { get; set; } = new();

    }
}
