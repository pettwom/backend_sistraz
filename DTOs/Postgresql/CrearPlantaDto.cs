namespace backend_trazabilidad.DTOs.Postgresql
{
    public class CrearPlantaDto
    {
        public string CodPlanta { get; set; } = string.Empty;
        public string DescPlanta { get; set; } = string.Empty;
        public string? ObsPlanta { get; set; } = string.Empty; 
        public string? Pais { get; set; } = string.Empty;
        public string? PuntoIngreso { get; set; } = string.Empty;
        public string? Departamento { get; set; } = string.Empty;
    }
}
