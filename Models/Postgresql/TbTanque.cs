using System;
using System.Collections.Generic;

namespace backend_trazabilidad.Models.Postgresql;

/// <summary>
/// Tanques físicos. Pueden recibir GLP de varias cisternas y alimentar varias engarrafadoras.
/// </summary>
public partial class TbTanque
{
    public long IdTanque { get; set; }

    public long IdInstancia { get; set; }

    public long? IdPlanta { get; set; }

    public decimal VolTotal { get; set; }

    public bool Estado { get; set; }

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

    public virtual TbPlantum? IdPlantaNavigation { get; set; }
}
