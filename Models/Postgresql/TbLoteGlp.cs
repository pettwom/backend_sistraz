using System;
using System.Collections.Generic;

namespace backend_trazabilidad.Models.Postgresql;

/// <summary>
/// Identidad del GLP trazable. El código del lote se mantiene a través de los eventos.
/// </summary>
public partial class TbLoteGlp
{
    public long IdLote { get; set; }

    public string Codigo { get; set; } = null!;

    public long IdPlantaOrigen { get; set; }

    public DateTime FechaOrigen { get; set; }

    public decimal VolumenInicial { get; set; }

    public string Unidad { get; set; } = null!;

    public string Estado { get; set; } = null!;

    public long Usucre { get; set; }

    public DateTime Feccre { get; set; }

    public virtual TbPlantum IdPlantaOrigenNavigation { get; set; } = null!;

    public virtual ICollection<TbEventoComposicion> TbEventoComposicions { get; set; } = new List<TbEventoComposicion>();

    public virtual ICollection<TbEventoDestino> TbEventoDestinos { get; set; } = new List<TbEventoDestino>();

    public virtual ICollection<TbEventoOrigen> TbEventoOrigens { get; set; } = new List<TbEventoOrigen>();
}
