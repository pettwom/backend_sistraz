namespace backend_trazabilidad.DTOs.Octano
{
    public class CalidadOctanoDto
    {
        public decimal IdPruebaCalidad { get; set; }

        public string PruebaEnsayo { get; set; } = string.Empty;

        public string Metodo { get; set; } = string.Empty;

        public string Unidad { get; set; } = string.Empty;

        public decimal? EspecMinima { get; set; }

        public decimal? EspecMaxima { get; set; }

        public string? EspecAlfanumerica { get; set; }
    }
}
