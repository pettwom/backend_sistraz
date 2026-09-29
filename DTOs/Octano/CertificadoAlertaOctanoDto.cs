using System.Text.Json.Serialization;

namespace backend_trazabilidad.DTOs.Octano
{
    public class CertificadoAlertaOctanoDto
    {
        [JsonPropertyName("ID_REGISTROCAL_PRINCIPAL")]
        public decimal IdRegistroCalPrincipal { get; set; }

        [JsonPropertyName("CITE_GENERADO")]
        public string CiteGenerado { get; set; } = "";

        [JsonPropertyName("FECHA_OPERACION")]
        public DateTime FechaOperacion { get; set; }

        [JsonPropertyName("NOMBRE_PRODUCTO")]
        public string? NombreProducto { get; set; }

        [JsonPropertyName("VALOR_LOTE")]
        public string? ValorLote { get; set; }

        [JsonPropertyName("VOLUMEN_OP_DEBE")]
        public decimal? Volumen { get; set; }

        [JsonPropertyName("CODIGO_UNIDAD_MEDIDA")]
        public string? UnidadMedida { get; set; }

        [JsonPropertyName("FECHA_REGISTRO")]
        public DateTime FechaRegistro { get; set; }

        [JsonPropertyName("USUARIO_REGISTRO")]
        public string? UsuarioRegistro { get; set; }

        [JsonPropertyName("ID_ENTIDAD")]
        public decimal IdEntidad { get; set; }

        [JsonPropertyName("ENTIDAD_DENOMINACION")]
        public string? Entidad { get; set; }

        [JsonPropertyName("ENTIDAD_DEST")]
        public string? EntidadDestino { get; set; }

        [JsonPropertyName("TIPO_ACTIVIDAD")]
        public string? TipoActividad { get; set; }

        [JsonPropertyName("OBSERVACIONES")]
        public string? Observaciones { get; set; }
    }
}
