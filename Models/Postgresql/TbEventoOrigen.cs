using System;
using System.Collections.Generic;

namespace backend_trazabilidad.Models.Postgresql;

/// <summary>
/// Detalle de orígenes. Permite múltiples orígenes en un mismo evento, por ejemplo 3 cisternas hacia un tanque.
/// </summary>
public partial class TbEventoOrigen
{
    public long IdEventoOrigen { get; set; }

    public long IdEvento { get; set; }

    public long IdInstancia { get; set; }

    public long IdLote { get; set; }

    public decimal Volumen { get; set; }

    public decimal? Porcentaje { get; set; }

    public string? Observacion { get; set; }

    public virtual TbEvento IdEventoNavigation { get; set; } = null!;

    public virtual TbInstancium IdInstanciaNavigation { get; set; } = null!;

    public virtual TbLoteGlp IdLoteNavigation { get; set; } = null!;
}
