using System;
using System.Collections.Generic;

namespace backend_trazabilidad.Models;

public partial class TblTrazabilidad
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

    /// <summary>
    /// estado del registro
    /// </summary>
    public bool Estado { get; set; }

    /// <summary>
    /// usuario de creacion del registro
    /// </summary>
    public int? Usucre { get; set; }

    /// <summary>
    /// fecha de creacion del registro
    /// </summary>
    public DateTime? Feccre { get; set; }

    /// <summary>
    /// usuario que modifico el registro
    /// </summary>
    public int? Usumod { get; set; }

    /// <summary>
    /// fecha que se modifico el registro
    /// </summary>
    public DateTime? Fecmod { get; set; }

    public virtual Despacho CorrDest1 { get; set; } = null!;

    public virtual Transporte CorrDest2 { get; set; } = null!;

    public virtual Almacenado CorrDestNavigation { get; set; } = null!;

    public virtual ICollection<Envasado> Envasados { get; set; } = new List<Envasado>();

    public virtual ICollection<Produccion> Produccions { get; set; } = new List<Produccion>();
}
