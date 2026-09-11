using System;
using System.Collections.Generic;

namespace OracleScanffold.Models.Oracle
{
    public partial class TsegUsuario
    {
        public TsegUsuario()
        {
            TsegPerfilesUsuarios = new HashSet<TsegPerfilesUsuario>();
        }

        public int IdUsuario { get; set; }
        public string Login { get; set; } = null!;
        public string Clave { get; set; } = null!;
        public bool? Estado { get; set; }
        public bool? AudEstado { get; set; }
        public string AudUsuario { get; set; } = null!;
        public DateTime AudFecha { get; set; }
        public string? ClaveSalt { get; set; }
        public decimal? AppIdUsuario { get; set; }
        public DateTime? VigenciaDesde { get; set; }
        public DateTime? VigenciaHasta { get; set; }

        public virtual ICollection<TsegPerfilesUsuario> TsegPerfilesUsuarios { get; set; }
    }
}
