using System;
using System.Collections.Generic;

namespace backend_trazabilidad.Models.Postgresql;

/// <summary>
/// Distribuidores o destinos de la última etapa de la cadena.
/// </summary>
public partial class TbDistribuidor
{
    public long IdDistribuidor { get; set; }

    public long IdInstancia { get; set; }

    public string? Ubicacion { get; set; }

    public bool Estado { get; set; }

    public string? NroFactura { get; set; }

    public int? NroGarrafas { get; set; }

    public decimal? VolEquivalente { get; set; }

    public string? PlacaDistribuidor { get; set; }

    public string? ZonaDistribucion { get; set; }

    public string? Observacin { get; set; }

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

    public virtual TbInstancium IdInstanciaNavigation { get; set; } = null!;
}
