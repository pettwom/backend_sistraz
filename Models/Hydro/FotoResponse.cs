using System.Text.Json.Serialization;

namespace backend_trazabilidad.Models.Hydro
{
    public class FotoResponse
    {
        [JsonPropertyName("oResultado")]
        public FotoResultado? oResultado { get; set; }
    }
    public class FotoResultado
    {
        [JsonPropertyName("archivO_BINARIO")]
        public string? ArchivoBinario { get; set; }
    }
}
