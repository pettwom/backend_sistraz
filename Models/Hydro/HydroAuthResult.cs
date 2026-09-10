namespace backend_trazabilidad.Models.Hydro
{
    public class HydroAuthResult
    {
        public decimal? IdUsuario { get; set; }
        public decimal? IdEntidad { get; set; } = default(decimal?);
        public string NombreCompleto { get; set; } = string.Empty;
        public string Perfil { get; set; } = string.Empty;
    }
}
