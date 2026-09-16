using System.Text.Json.Serialization;

namespace backend_trazabilidad.DTOs.Postgresql
{
    public class CrearPlantaDto
    {
        [JsonPropertyName("cod_planta")]
        public string CodPlanta { get; set; } = string.Empty;
        [JsonPropertyName("desc_planta")]
        public string DescPlanta { get; set; } = string.Empty;
        [JsonPropertyName("obs_planta")]
        public string? ObsPlanta { get; set; } = string.Empty; 
        [JsonPropertyName("pais")]
        public string? Pais { get; set; } = string.Empty;
        [JsonPropertyName("punto_ingreso")]
        public string? PuntoIngreso { get; set; } = string.Empty;
        [JsonPropertyName("ciudad")]
        public string? Departamento { get; set; } = string.Empty;
    }
}
