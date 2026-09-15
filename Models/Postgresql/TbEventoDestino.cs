using System;
using System.Collections.Generic;

namespace backend_trazabilidad.Models.Postgresql;

/// <summary>
/// Detalle de destinos. Permite múltiples destinos en un evento, por ejemplo un tanque hacia 3 engarrafadoras.
/// </summary>
public partial class TbEventoDestino
{
    public long IdEventoDestino { get; set; }

    public long IdEvento { get; set; }

    public long IdInstancia { get; set; }

    public long IdLote { get; set; }

    public decimal Volumen { get; set; }

    public decimal? Porcentaje { get; set; }

    public virtual TbEvento IdEventoNavigation { get; set; } = null!;

    public virtual TbInstancium IdInstanciaNavigation { get; set; } = null!;

    public virtual TbLoteGlp IdLoteNavigation { get; set; } = null!;

    public virtual ICollection<TbEventoComposicion> TbEventoComposicions { get; set; } = new List<TbEventoComposicion>();
}
