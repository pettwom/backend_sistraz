using System;
using System.Collections.Generic;

namespace backend_trazabilidad.Models.Postgresql;

public partial class Transporte
{
    /// <summary>
    /// identificador unico de la tabla transporte
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// numero correlativo 
    /// </summary>
    public int Correlativo { get; set; }

    /// <summary>
    /// nombre del operador
    /// </summary>
    public string Operador { get; set; } = null!;

    /// <summary>
    /// numero de certificado de calidad
    /// </summary>
    public string NumCert { get; set; } = null!;

    /// <summary>
    /// volumen transportado 
    /// </summary>
    public int VolTransportado { get; set; }

    /// <summary>
    /// fecha y hora de carga del glp 
    /// </summary>
    public DateTime FechaHoraCarga { get; set; }

    /// <summary>
    /// numero de placa del transporte
    /// </summary>
    public string? Placa { get; set; }

    /// <summary>
    /// nombre del conductor 
    /// </summary>
    public string Conductor { get; set; } = null!;

    /// <summary>
    /// numero del precinto
    /// </summary>
    public string? NumPrecinto { get; set; }

    /// <summary>
    /// observacion
    /// </summary>
    public string? Observacion { get; set; }

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
