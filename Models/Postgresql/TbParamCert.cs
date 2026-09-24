using System;
using System.Collections.Generic;

namespace backend_trazabilidad.Models.Postgresql;

public partial class TbParamCert
{
    public int Id { get; set; }

    public int? IdCertEspec { get; set; }

    public int? AstmUop { get; set; }

    public string? UniParams { get; set; }

    public string? ValorReportado { get; set; }

    public string? Justificacion { get; set; }

    public string? Estado { get; set; }

    public string? Matadata { get; set; }

    public bool Activo { get; set; }

    public DateTime CreadoEn { get; set; }

    public DateTime? ActualizadoEn { get; set; }

    public DateTime? EliminadoEn { get; set; }

    public long? IdCreadoPor { get; set; }

    public long? IdActualizadoPor { get; set; }

    public long? IdEliminadoPor { get; set; }

    public string? CreadoPor { get; set; }

    public string? ActualizadoPor { get; set; }

    public string? EliminadoPor { get; set; }

    public virtual TbCertEspec? IdCertEspecNavigation { get; set; }

    public virtual ICollection<TbCertificado> TbCertificados { get; set; } = new List<TbCertificado>();
}
