using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OracleScanffold.Models.Oracle;

public class TparPais
{
    public long IdPais { get; set; }
    public string? Descripcion { get; set; }
    public string? Abreviacion2 { get; set; }
    public string? Abreviacion3 { get; set; }
    public DateTime? FechaDesde { get; set; }
    public DateTime? FechaHasta { get; set; }
    public short AudEstado { get; set; }
    public string AudUsuario { get; set; } = null!;
    public DateTime AudFecha { get; set; }
}
