using System.Diagnostics.Eventing.Reader;

namespace backend_trazabilidad.DTOs.Postgresql
{
    public class CrearCisternasDto
    {
        public long IdInstancia { get; set; }
        public long IdCisternaDetalle { get; set; }
        public string Estado { get; set; } = string.Empty;
        public DateTime CreadoEn { get; set; }
        public long IdCreadoPor { get; set; }
        public string CreadorPor { get; set; } = string.Empty;
    }
}
