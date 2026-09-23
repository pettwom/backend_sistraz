using System.Text.Json.Nodes;

namespace backend_trazabilidad.DTOs.Postgresql
{
    public class ProdCisternaDto
    {
        public long IdPlanta { get; set; }
        public JsonArray Cisterna { get; set; } = new();
    }

}
