using System;
using System.Collections.Generic;

namespace backend_trazabilidad.Models;

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
    /// volumen total de la planta o operador
    /// </summary>
    public int VolTotal { get; set; }

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

    /// <summary>
    /// usuario de creacion del registro 
    /// </summary>
    public int Usucre { get; set; }

    /// <summary>
    /// fecha de creacion del registro
    /// </summary>
    public DateTime Feccre { get; set; }

    /// <summary>
    /// usuario que modifico el registro
    /// </summary>
    public int? Usumod { get; set; }

    /// <summary>
    /// fecha de modificacion del regitro
    /// </summary>
    public DateTime? Fecmod { get; set; }

    /// <summary>
    /// estado del registro
    /// </summary>
    public bool Estado { get; set; }

    public virtual TblTrazabilidad? TblTrazabilidad { get; set; }
}
