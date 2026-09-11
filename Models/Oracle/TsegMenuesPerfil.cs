using System;
using System.Collections.Generic;

namespace OracleScanffold.Models.Oracle
{
    public partial class TsegMenuesPerfil
    {
        public int IdMenuPerfil { get; set; }
        public short IdPerfil { get; set; }
        public int IdMenu { get; set; }
        public short IdPrivilegio { get; set; }
        public bool? AudEstado { get; set; }
        public string AudUsuario { get; set; } = null!;
        public DateTime AudFecha { get; set; }
        public decimal? Prioridad { get; set; }
        public decimal? AppIdUsuario { get; set; }

        public virtual TsegMenue IdMenuNavigation { get; set; } = null!;
        public virtual TsegPerfile IdPerfilNavigation { get; set; } = null!;
    }
}
