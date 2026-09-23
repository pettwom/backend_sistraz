using System;
using System.Collections.Generic;

namespace backend_trazabilidad.Models.PostgresqlPrueba;

public partial class TbCisternaDetalle
{
    public int Id { get; set; }

    public DateTime? Fecha { get; set; }

    public string? NroCre { get; set; }

    public string? NroCreFenix { get; set; }

    public decimal? PesoKg { get; set; }

    public decimal? PesoTm { get; set; }

    public decimal? VolM3 { get; set; }

    public decimal? VolBbls { get; set; }

    public decimal? Gravedad { get; set; }

    public string? Cliente { get; set; }

    public string? PlantaDescarga { get; set; }

    public string? Conductor { get; set; }

    public string? Placa { get; set; }

    public string? EmpresaTrans { get; set; }

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
}
