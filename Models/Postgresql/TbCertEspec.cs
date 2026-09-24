using System;
using System.Collections.Generic;

namespace backend_trazabilidad.Models.Postgresql;

public partial class TbCertEspec
{
    public int Id { get; set; }

    public string? DescEspec { get; set; }

    public string? MinEspec { get; set; }

    public string? MaxEspec { get; set; }

    public string? Unidad { get; set; }

    public string? MetAltern1 { get; set; }

    public string? MetAltern2 { get; set; }

    public string? Estado { get; set; }

    public string? Matadata { get; set; }

    public bool Activo { get; set; }

    public DateTime CreadoEn { get; set; }

    public DateTime? ActualizadoEn { get; set; }

    public DateTime? EliminadoEn { get; set; }

    public long? IdCreadoPor { get; set; }

    public long? IdActualizadoPor { get; set; }

    public long? IdEliminadoPor { get; set; }

    public string? CreadoPor { get; set; }

    public string? ActualizadoPor { get; set; }

    public string? EliminadoPor { get; set; }

    public virtual ICollection<TbParamCert> TbParamCerts { get; set; } = new List<TbParamCert>();
}
