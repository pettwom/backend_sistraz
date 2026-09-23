using Microsoft.AspNetCore.Http.Connections;

namespace backend_trazabilidad.DTOs.Postgresql
{
    public class CisternaExcelDto
    {
        public long? Id { get; set; }
        public DateTime Fecha { get; set; }
        public string NroCre { get; set; } = string.Empty;
        public string NroCreFenix { get; set; } = string.Empty;
        public long PesoKg { get; set; }
        public long PesoTm { get; set; }
        public long VolM3 { get; set; }
        public long VolBbls { get; set; }
        public decimal Gravedad { get; set; }
        public string Cliente { get; set; } = string.Empty;
        public string PlantaDescarga { get; set; } = string.Empty;
        public string Conductor { get; set; } = string.Empty;
        public string Placa { get; set; } = string.Empty;
        public string EmpresaTrans { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
    }
}
