using System.Text.Json.Serialization;

namespace backend_trazabilidad.Models.Hydro
{
    public class ListaFuncionariosResponse
    {
        [JsonPropertyName("oResultado")]
        public List<FuncionarioDto>? oResultado { get; set; } = new();
    }
}
