namespace backend_trazabilidad.DTOs.Oracle
{
    public class MenuDto
    {
        public decimal IdMenu { get; set; }
        public string? Titulo { get; set; }
        public string? Enlace { get; set; }
        public decimal? IdMenuPadre { get; set; }
        public string? Icono { get; set; }
        public decimal? Orden {  get; set; }
        public string? Descripcion { get; set; }

    }
}
