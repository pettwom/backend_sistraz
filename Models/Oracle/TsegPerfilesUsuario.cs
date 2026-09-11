using System;
using System.Collections.Generic;

namespace OracleScanffold.Models.Oracle
{
    public partial class TsegPerfilesUsuario
    {
        public int IdPerfilUsuario { get; set; }
        public int IdUsuario { get; set; }
        public short IdPerfil { get; set; }
        public short? AudEstado { get; set; }
        public string AudUsuario { get; set; } = null!;
        public DateTime AudFecha { get; set; }
        public short? EstadoPerfil { get; set; }
        public decimal? AppIdUsuario { get; set; }

        public virtual TsegPerfile IdPerfilNavigation { get; set; } = null!;
        public virtual TsegUsuario IdUsuarioNavigation { get; set; } = null!;
    }
}
