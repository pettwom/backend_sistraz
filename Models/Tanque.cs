using System;
using System.Collections.Generic;

namespace backend_trazabilidad.Models;

public partial class Tanque
{
    /// <summary>
    /// identificador unico
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// nombre del tanque
    /// </summary>
    public string NomTanque { get; set; } = null!;

    /// <summary>
    /// volumen ingresado al tanque
    /// </summary>
    public int VolIngresado { get; set; }

    /// <summary>
    /// volumen total despachado
    /// </summary>
    public int? VolTotDespachado { get; set; }

    /// <summary>
    /// saldo disponible del tanque
    /// </summary>
    public int SaldoDisp { get; set; }

    /// <summary>
    /// observacion
    /// </summary>
    public string? Observacion { get; set; }

    /// <summary>
    /// estado del registro
    /// </summary>
    public bool Estado { get; set; }

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
    /// fecha de modificacion del registro
    /// </summary>
    public DateTime? Fecmod { get; set; }

    public virtual ICollection<Almacenado> Almacenados { get; set; } = new List<Almacenado>();
}
