using System;
using System.Collections.Generic;

namespace backend_trazabilidad.Models.Postgresql;

public partial class TbCertificado
{
    public long IdCertificado { get; set; }

    public long IdEvento { get; set; }

    public int? IdCertParam { get; set; }

    public string NumeroCertificado { get; set; } = null!;

    public string TipoCertificado { get; set; } = null!;

    public string? Tag { get; set; }

    public DateTime? FechaMuestreo { get; set; }

    public decimal? VolumenLote { get; set; }

    public string? UnidadMed { get; set; }

    public string? DocumentoUrl { get; set; }

    public string? TamCert { get; set; }

    public string? Observacion { get; set; }

    public bool Estado { get; set; }

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

    public virtual TbParamCert? IdCertParamNavigation { get; set; }
}
