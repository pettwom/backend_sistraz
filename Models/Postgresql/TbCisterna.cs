using System;
using System.Collections.Generic;

namespace backend_trazabilidad.Models.Postgresql;

/// <summary>
/// Unidades de transporte de GLP.
/// </summary>
public partial class TbCisterna
{
    public long IdCisterna { get; set; }

    public long IdInstancia { get; set; }

    public long IdCisternaDetalle { get; set; }

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
}
