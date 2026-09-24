using System.Text.Json.Nodes;

namespace backend_trazabilidad.DTOs.Postgresql
{
    public class EventoRequestDto
    {
        public long IdInstancia { get; set; }
        public string TipoAccion { get; set; } = string.Empty;
        public string TipoEvento { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public List<DataDto> Data { get; set; } = new();
        public string EtapaFlujo { get; set; } = string.Empty;
    }
    public class DataDto
    {
        public long Id { get; set; }
    }
}
