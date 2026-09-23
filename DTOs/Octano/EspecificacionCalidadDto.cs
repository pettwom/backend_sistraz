namespace backend_trazabilidad.DTOs.Octano
{
    public class EspecificacionCalidadDto
    {
        public decimal IdProducto { get; set; }

        public decimal IdTablaEspec { get; set; }

        public string Nombre { get; set; } = string.Empty;
    }
}
