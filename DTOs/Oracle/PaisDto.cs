namespace backend_trazabilidad.DTOs.Oracle
{
    public class PaisDto
    {
        public long IdPais { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public string Abreviacion2 { get; set; } = string.Empty;
        public string Abreviacion3 { get; set; } = string.Empty;
    }
}
