namespace backend_trazabilidad.DTOs.Oracle
{
    public class ParametroCalidadDto
    {
        public decimal IdPruebaCalidad { get; set; }

        public decimal? IdPruebaCalidadPadre { get; set; }

        public string Descripcion { get; set; }
            = string.Empty;

        public string? EspecMinima { get; set; }

        public string? EspecMaxima { get; set; }

        public string? EspecAlfanumerico { get; set; }

        public string? MetodoAstm { get; set; }

        public string? UnidadMedida { get; set; }

        public string? RangosMultiples { get; set; }

        public string? EspecMinimaRango { get; set; }

        public string? EspecMaximaRango { get; set; }
    }
}
