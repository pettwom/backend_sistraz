using System;
using System.Collections.Generic;

namespace backend_trazabilidad.Models;

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
    /// estado del registro 1= activo; 2= inactivo
    /// </summary>
    public DateTime? Estado { get; set; }

    /// <summary>
    /// usuario que creo el registro
    /// </summary>
    public int Usucre { get; set; }

    /// <summary>
    /// fecha de creacion del registro
    /// </summary>
    public DateTime Feccre { get; set; }

    /// <summary>
    /// usuario de modificacion
    /// </summary>
    public int? Usumod { get; set; }

    /// <summary>
    /// fecha de modificacion del registro
    /// </summary>
    public DateTime? Fecmod { get; set; }

    /// <summary>
    /// numero correlativo enlazado a tbl_trazabilidad
    /// </summary>
    public int? Correlativo { get; set; }

    public virtual TblTrazabilidad? CorrelativoNavigation { get; set; }

    public virtual Plantum? Plantum { get; set; }
}
