using System.ComponentModel.DataAnnotations;

namespace backend_trazabilidad.DTOs.Postgresql
{
    public class ProduccionDto
    {

        public string Lote { get; set; } = string.Empty;

        public string NumCertificacion { get; set; } = string.Empty;

        public DateTime? FechaMuestreo { get; set; }
  
        public int VolTotal { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string Tipo { get; set; } = string.Empty;

        public string Pais { get; set; } = string.Empty;

        public string PuntoIngreso { get; set; } = string.Empty;
    }
}
