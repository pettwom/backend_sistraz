namespace backend_trazabilidad.DTOs.Postgresql
{
    public class CargaOperadorDto
    {
        public long IdOperador { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string? Pais { get; set; } = string.Empty;
        public string? PuntoIngreso { get; set; } = string.Empty;
    }
}
