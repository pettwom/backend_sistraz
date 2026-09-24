using System;
using System.Collections.Generic;

namespace backend_trazabilidad.Models.Postgresql;

public partial class TbEventoDestino
{
    public long IdEventoDestino { get; set; }

    public long IdEvento { get; set; }

    public long IdInstancia { get; set; }

    public long IdLote { get; set; }

    public decimal Volumen { get; set; }

    public decimal? Porcentaje { get; set; }

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

    public virtual TbEvento IdEventoNavigation { get; set; } = null!;

    public virtual TbInstancium IdInstanciaNavigation { get; set; } = null!;

    public virtual TbLoteGlp IdLoteNavigation { get; set; } = null!;

    public virtual ICollection<TbEventoComposicion> TbEventoComposicions { get; set; } = new List<TbEventoComposicion>();
}
