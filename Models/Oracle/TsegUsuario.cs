using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OracleScanffold.Models.Oracle
{
    public partial class TsegUsuario
    {
        public TsegUsuario()
        {
            TsegPerfilesUsuarios = new HashSet<TsegPerfilesUsuario>();
        }
        [Key]
        public int IdUsuario { get; set; }
        public string Login { get; set; } = null!;
        public string Clave { get; set; } = null!;
        public short? Estado { get; set; }
        public short? AudEstado { get; set; }
        public string AudUsuario { get; set; } = null!;
        public DateTime AudFecha { get; set; }
        public string? ClaveSalt { get; set; }
        public decimal? AppIdUsuario { get; set; }
        public DateTime? VigenciaDesde { get; set; }
        public DateTime? VigenciaHasta { get; set; }

        public virtual ICollection<TsegPerfilesUsuario> TsegPerfilesUsuarios { get; set; }


    }
}
