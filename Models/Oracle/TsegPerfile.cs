using System;
using System.Collections.Generic;

namespace OracleScanffold.Models.Oracle
{
    public partial class TsegPerfile
    {
        public TsegPerfile()
        {
            InverseIdPerfilPadreNavigation = new HashSet<TsegPerfile>();
            TsegMenuesPerfils = new HashSet<TsegMenuesPerfil>();
            TsegPerfilesUsuarios = new HashSet<TsegPerfilesUsuario>();
        }

        public short IdPerfil { get; set; }
        public string Descripcion { get; set; } = null!;
        public bool? AudEstado { get; set; }
        public string AudUsuario { get; set; } = null!;
        public DateTime AudFecha { get; set; }
        public decimal? Prioridad { get; set; }
        public short? IdPerfilPadre { get; set; }
        public string? NombrePerfil { get; set; }
        public decimal? ModuloId { get; set; }
        public decimal? AppIdUsuario { get; set; }

        public virtual TsegPerfile? IdPerfilPadreNavigation { get; set; }
        public virtual ICollection<TsegPerfile> InverseIdPerfilPadreNavigation { get; set; }
        public virtual ICollection<TsegMenuesPerfil> TsegMenuesPerfils { get; set; }
        public virtual ICollection<TsegPerfilesUsuario> TsegPerfilesUsuarios { get; set; }
    }
}
