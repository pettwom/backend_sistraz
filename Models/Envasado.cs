using System;
using System.Collections.Generic;

namespace backend_trazabilidad.Models;

public partial class Envasado
{
    /// <summary>
    /// identificador unico
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// correlativo
    /// </summary>
    public int Correlativo { get; set; }

    /// <summary>
    /// fecha del envasado del glp
    /// </summary>
    public DateTime FechaEnvasado { get; set; }

    /// <summary>
    /// nombre del operador
    /// </summary>
    public string Operador { get; set; } = null!;

    /// <summary>
    /// numero del reporte del envasado
    /// </summary>
    public string? NumReporte { get; set; }

    /// <summary>
    /// volumen del envasado
    /// </summary>
    public int VolEnvasado { get; set; }

    /// <summary>
    /// unidad de medida del envasado
    /// </summary>
    public string UnidadEnvasado { get; set; } = null!;

    /// <summary>
    /// numero de garrafas
    /// </summary>
    public int? NumGarrafas { get; set; }

    /// <summary>
    /// peso unitario por garrafa
    /// </summary>
    public int? PesoUnitario { get; set; }

    /// <summary>
    /// unidad de medida del peso
    /// </summary>
    public string? UnidadPeso { get; set; }

    /// <summary>
    /// estado de hermeticidad
    /// </summary>
    public string EstadoHermeticidad { get; set; } = null!;

    /// <summary>
    /// volumen despachado
    /// </summary>
    public int? VolDespachado { get; set; }

    /// <summary>
    /// unidad de medida del volumen depachado
    /// </summary>
    public string? UnidadDespacho { get; set; }

    /// <summary>
    /// saldo del volumen disponible
    /// </summary>
    public int? SaldoDisponible { get; set; }

    /// <summary>
    /// unidad de medida del volumen saldo disponible
    /// </summary>
    public string? UnidadDisponible { get; set; }

    /// <summary>
    /// observacion
    /// </summary>
    public string? Observacion { get; set; }

    /// <summary>
    /// estado del registro 1=activo; 2=inactivo
    /// </summary>
    public bool Estado { get; set; }

    /// <summary>
    /// usuario que creo el registro
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
    /// fecha que se modifico el registro
    /// </summary>
    public DateTime? Fecmod { get; set; }

    public virtual TblTrazabilidad CorrelativoNavigation { get; set; } = null!;
}
