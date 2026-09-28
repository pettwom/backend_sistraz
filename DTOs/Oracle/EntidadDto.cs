namespace backend_trazabilidad.DTOs.Oracle
{
    public class EntidadDto
    {
        public long IdEntidad { get; set; }
        public decimal? IdActividad { get; set; }
        public decimal? IdMunicipio { get; set; }
        public decimal? IdDepartamento { get; set; }
        public string Denominacion { get; set; } = string.Empty;
        public string Actividad { get; set; } = string.Empty;
        public string Telefonos { get; set; } = string.Empty;
        public string AmbitoOperacion { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string Departamento { get; set; } = string.Empty;
        public string Municipio { get; set; } = string.Empty;
        public string Localidad { get; set; } = string.Empty;
    }
}
