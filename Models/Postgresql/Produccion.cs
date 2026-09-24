using System;
using System.Collections.Generic;

namespace backend_trazabilidad.Models.Postgresql;

public partial class Produccion
{
    /// <summary>
    /// identificador unico para produccion
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Identificador de la planta o operador
    /// </summary>
    public int IdPlanta { get; set; }

    /// <summary>
    /// numero de certificado de calidad
    /// </summary>
    public string NumCertCal { get; set; } = null!;

    /// <summary>
    /// observacion de la produccion
    /// </summary>
    public string? Observacion { get; set; }

    /// <summary>
    /// numero correlativo enlazado a tbl_trazabilidad
    /// </summary>
    public int? Correlativo { get; set; }

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

    public DateTime? FechaMuestreo { get; set; }
}
