using System.Text.Json.Nodes;

namespace backend_trazabilidad.DTOs.Postgresql
{
    public class ProdCisternaDto
    {
        public long IdPlanta { get; set; }
        public List<CisternaSeleccionadasDto> Cisterna { get; set; } = new();
    }
    public class CisternaSeleccionadasDto
    { 
        public long Id { get; set; }
    }
}
