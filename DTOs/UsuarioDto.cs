namespace backend_trazabilidad.DTOs
{
    public class UsuarioDto
    {
        public decimal IdUsuarioHydro { get; set; }

        public decimal? IdEntidad { get; set; }

        public string NombreCompleto { get; set; } = string.Empty;

        public string Correo { get; set; } = string.Empty;

        public string Entidad { get; set; } = string.Empty;

        public string? Direccion { get; set; }

        public string? DenominacionUnidad { get; set; }

        public string? Cargo { get; set; }

        public string? Ci { get; set; }

        public decimal? IdOrganigrama { get; set; }

        public string? Fotografia { get; set; }

        public int CantidadBandeja { get; set; } = 0;

        public List<string> Perfiles { get; set; } = new();
    }
}
