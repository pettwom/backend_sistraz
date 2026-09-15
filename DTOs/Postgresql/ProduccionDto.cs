namespace backend_trazabilidad.DTOs.Postgresql
{
    public class ProduccionDto
    {
        public string Lote { get; set; } = string.Empty;

        public string Nombre { get; set; } = string.Empty;
        public string NumCertificado { get; set; } = string.Empty;
        public DateTime FechaMuestreo { get; set; }
        public decimal VolTotal { get; set; }
        public string Pais { get; set; } = string.Empty;
        public string PuntoIngreso { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
    }
}
