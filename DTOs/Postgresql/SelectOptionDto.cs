namespace backend_trazabilidad.DTOs.Postgresql
{
    public class SelectOptionDto
    {
        public long Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string Valor { get; set; } = string.Empty;
        public short Estado { get; set; }
        public string Categoria { get; set; } = string.Empty;
    }
}
