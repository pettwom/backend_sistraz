using System;
using System.Collections.Generic;

namespace backend_trazabilidad.Models.Postgresql;

public partial class TbEvento
{
    public long IdEvento { get; set; }

    public string TipoEvento { get; set; } = null!;

    public DateTime FechaEvento { get; set; }

    public string? Descripcion { get; set; }

    public string Estado { get; set; } = null!;

    public string? Observacion { get; set; }

    public long Usucre { get; set; }

    public DateTime Feccre { get; set; }

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

    public virtual ICollection<TbEventoDestino> TbEventoDestinos { get; set; } = new List<TbEventoDestino>();

    public virtual ICollection<TbEventoOrigen> TbEventoOrigens { get; set; } = new List<TbEventoOrigen>();
}
