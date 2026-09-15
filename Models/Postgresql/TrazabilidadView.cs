using System;
using System.Collections.Generic;

namespace backend_trazabilidad.Models.Postgresql;

public partial class TrazabilidadView
{
    public long? IdLote { get; set; }

    public string? CodigoTrazabilidad { get; set; }

    public long? IdEvento { get; set; }

    public string? TipoEvento { get; set; }

    public DateTime? FechaEvento { get; set; }

    public string? Estado { get; set; }

    public string? Origenes { get; set; }

    public string? Destinos { get; set; }

    public string? Certificados { get; set; }
}
