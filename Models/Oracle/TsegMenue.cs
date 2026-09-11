using System;
using System.Collections.Generic;

namespace OracleScanffold.Models.Oracle
{
    public partial class TsegMenue
    {
        public TsegMenue()
        {
            InverseIdMenuPadreNavigation = new HashSet<TsegMenue>();
            TsegMenuesPerfils = new HashSet<TsegMenuesPerfil>();
        }

        public int IdMenu { get; set; }
        public string Titulo { get; set; } = null!;
        public string? Abreviacion { get; set; }
        public string? Enlace { get; set; }
        public byte[]? Icono { get; set; }
        public int? IdMenuPadre { get; set; }
        public short IdModulo { get; set; }
        public bool? AudEstado { get; set; }
        public string AudUsuario { get; set; } = null!;
        public DateTime AudFecha { get; set; }
        public decimal Orden { get; set; }
        public decimal? AppIdUsuario { get; set; }
        public decimal? Nivel { get; set; }
        public string? Descripcion { get; set; }

        public virtual TsegMenue? IdMenuPadreNavigation { get; set; }
        public virtual ICollection<TsegMenue> InverseIdMenuPadreNavigation { get; set; }
        public virtual ICollection<TsegMenuesPerfil> TsegMenuesPerfils { get; set; }
    }
}
