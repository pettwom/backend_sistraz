using System;
using System.Collections.Generic;

namespace backend_trazabilidad.Models.Postgresql;

/// <summary>
/// Hecho ocurrido con el GLP. Agrupa sus orígenes, destinos y certificados.
/// </summary>
public partial class TbEvento
{
    public long IdEvento { get; set; }

    public string TipoEvento { get; set; } = null!;

    public DateTime FechaEvento { get; set; }

    public string? Descripcion { get; set; }

    public string Estado { get; set; } = null!;

    public string? Observacion { get; set; }

    public long Usucre { get; set; }

    public DateTime Feccre { get; set; }

    public virtual ICollection<TbCertificado> TbCertificados { get; set; } = new List<TbCertificado>();

    public virtual ICollection<TbEventoDestino> TbEventoDestinos { get; set; } = new List<TbEventoDestino>();

    public virtual ICollection<TbEventoOrigen> TbEventoOrigens { get; set; } = new List<TbEventoOrigen>();
}
