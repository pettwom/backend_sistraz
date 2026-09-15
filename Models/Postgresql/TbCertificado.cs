using System;
using System.Collections.Generic;

namespace backend_trazabilidad.Models.Postgresql;

/// <summary>
/// Certificados asociados a cada evento/etapa de la trazabilidad.
/// </summary>
public partial class TbCertificado
{
    public long IdCertificado { get; set; }

    public long IdEvento { get; set; }

    public string NumeroCertificado { get; set; } = null!;

    public string TipoCertificado { get; set; } = null!;

    public DateTime FechaEmision { get; set; }

    public DateTime? FechaMuestreo { get; set; }

    public decimal? VolumenMuestreado { get; set; }

    public string? Resultado { get; set; }

    public string? Laboratorio { get; set; }

    public string? DocumentoUrl { get; set; }

    public string? HashDocumento { get; set; }

    public bool Estado { get; set; }

    public long Usucre { get; set; }

    public DateTime Feccre { get; set; }

    public virtual TbEvento IdEventoNavigation { get; set; } = null!;
}
