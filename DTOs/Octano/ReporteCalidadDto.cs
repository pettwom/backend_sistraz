namespace backend_trazabilidad.DTOs.Octano
{
    public class ReporteCalidadDto
    {
        public decimal? IdRegistroCalidad { get; set; }

        public decimal? IdTablaEspec { get; set; }

        public decimal? IdPruebaCalidad { get; set; }

        public string? Descripcion { get; set; }

        public string? CodigoAstm { get; set; }

        public string? CodigoUnidad { get; set; }

        public string? ValorAlfanumerico { get; set; }

        public decimal? IdPuntoCustodio { get; set; }

        public string? PuntoCustodio { get; set; }

        public decimal? Volumen { get; set; }

        public decimal? IdProducto { get; set; }

        public string? Producto { get; set; }

        public string? CodigoUnidadVolumen { get; set; }

        public string? CiteDocumento { get; set; }

        public decimal? IdEntidad { get; set; }

        public string? Entidad { get; set; }

        public DateTime? FechaOperacion { get; set; }

        public string? ValorLote { get; set; }

        public string? Observaciones { get; set; }

        public string? ProductoPadre { get; set; }

        public decimal? IdTipoActividad { get; set; }

        public string? TipoActividad { get; set; }

        public string? RutaInternacion { get; set; }

        public string? EmpresaProveedora { get; set; }

        public string? TanqueExterno { get; set; }

        public string? NroLoteVerif { get; set; }
    }
}
