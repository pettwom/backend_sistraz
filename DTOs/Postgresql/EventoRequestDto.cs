using System.Text.Json.Nodes;

namespace backend_trazabilidad.DTOs.Postgresql
{
    public class EventoRequestDto
    {
        public long IdPlanta { get; set; }
        //public List<InstanciaDto> IdInstancia { get; set; } = new();
        public long IdInstancia { get; set; }
        public string TipoAccion { get; set; } = string.Empty;
        public string TipoEvento { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public long Data { get; set; } 
        //public List<DataDto> Data { get; set; } = new();
        public string EtapaFlujo { get; set; } = string.Empty;
        public long CantEvento { get; set; }
    }
    //public class DataDto
    //{
    //    public long Id { get; set; }
    //}
}
