using System.Text.Json.Serialization;

namespace backend_trazabilidad.Models.Hydro
{
    public class FuncionarioResponse
    {
        [JsonPropertyName("oResultado")]
        public FuncionarioDto? oResultado { get; set; }
    }
}
