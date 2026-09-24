using System;
using System.Collections.Generic;

namespace backend_trazabilidad.Models.Postgresql;

/// <summary>
/// Instalaciones de engarrafado que reciben GLP desde uno o varios tanques.
/// </summary>
public partial class TbEngarrafadora
{
    public long IdEngarrafadora { get; set; }

    public long IdInstancia { get; set; }

    public decimal? CapacidadDiaria { get; set; }

    public bool Estado { get; set; }

    public int? VolDisponible { get; set; }

    public int? VolEnvasado { get; set; }

    public string? Observacion { get; set; }

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

    public virtual TbInstancium IdInstanciaNavigation { get; set; } = null!;
}
