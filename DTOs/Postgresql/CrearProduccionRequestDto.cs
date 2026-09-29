namespace backend_trazabilidad.DTOs.Postgresql
{
    public class CrearProduccionRequestDto
    {
        public long PlantaId { get; set; }
        public string? NroCertificado { get; set; } = string.Empty;
        public decimal VolTotal { get; set; }
        public DateTime FechaMuestra { get; set; }
        public string? Observacion { get; set; } = string.Empty;
        public string Departamento { get; set; } = string.Empty;
        public short Tipo { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public string? PaisImpor { get; set; } = string.Empty;
        public string? PuntoIngreso { get; set; } = string.Empty;
    }
}
