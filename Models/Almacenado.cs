using System;
using System.Collections.Generic;

namespace backend_trazabilidad.Models;

public partial class Almacenado
{
    /// <summary>
    /// identificador unico
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// correlativo
    /// </summary>
    public int? Correlativo { get; set; }

    /// <summary>
    /// nombre del operador
    /// </summary>
    public string NombreOperador { get; set; } = null!;

    /// <summary>
    /// nombre completo del responsable
    /// </summary>
    public string NomResponsable { get; set; } = null!;

    /// <summary>
    /// volumen recibido
    /// </summary>
    public int VolRecibido { get; set; }

    /// <summary>
    /// numero de certificacion de calidad
    /// </summary>
    public string NumCert { get; set; } = null!;

    /// <summary>
    /// nota de observacion
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

    /// <summary>
    /// identificador unico del tanque
    /// </summary>
    public int IdTanque { get; set; }

    public virtual Tanque IdTanqueNavigation { get; set; } = null!;

    public virtual TblTrazabilidad? TblTrazabilidad { get; set; }
}
