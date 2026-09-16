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

    public virtual ICollection<TbEventoComposicion> TbEventoComposicions { get; set; }
    = new List<TbEventoComposicion>();

    public virtual ICollection<TbEventoDestino> TbEventoDestinos { get; set; }
        = new List<TbEventoDestino>();

    public virtual ICollection<TbEventoOrigen> TbEventoOrigens { get; set; }
        = new List<TbEventoOrigen>();
}
