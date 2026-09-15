using System;
using System.Collections.Generic;

namespace backend_trazabilidad.Models.Postgresql;

/// <summary>
/// Genealogía de mezclas: indica cuánto de cada lote compone un destino.
/// </summary>
public partial class TbEventoComposicion
{
    public long IdComposicion { get; set; }

    public long IdEventoDestino { get; set; }

    public long IdLoteOrigen { get; set; }

    public decimal Volumen { get; set; }

    public decimal? Porcentaje { get; set; }

    public virtual TbEventoDestino IdEventoDestinoNavigation { get; set; } = null!;

    public virtual TbLoteGlp IdLoteOrigenNavigation { get; set; } = null!;
}
