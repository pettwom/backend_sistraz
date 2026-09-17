namespace backend_trazabilidad.DTOs.Postgresql
{
    public class SelectOptionDto
    {
        public long IdPlanta { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Departamento { get; set; } = string.Empty;
    }
}
