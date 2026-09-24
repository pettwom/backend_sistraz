using System;
using System.Collections.Generic;

namespace backend_trazabilidad.Models.Postgresql;

public partial class TblTrazabilidad1
{
    /// <summary>
    /// identificador unico
    /// </summary>
    public int Id { get; set; }

    public int IdPlanta { get; set; }

    /// <summary>
    /// lugar del flujo
    /// </summary>
    public string Lugar { get; set; } = null!;

    public string MesAnio { get; set; } = null!;

    /// <summary>
    /// correlativo de origen
    /// </summary>
    public int CorrOri { get; set; }

    /// <summary>
    /// correlativo destino
    /// </summary>
    public int CorrDest { get; set; }

    public string? Metadata { get; set; }

    public bool Activo { get; set; }

    public DateTime CreadoEn { get; set; }

    public DateTime? ActualizadoEn { get; set; }

    public DateTime? EliminadoEn { get; set; }

    public long IdCreadoPor { get; set; }

    public long? IdActualizadoPor { get; set; }

    public long? IdEliminadoPor { get; set; }

    public string CreadoPor { get; set; } = null!;

    public string? ActualizadoPor { get; set; }

    public string? EliminadoPor { get; set; }
}
