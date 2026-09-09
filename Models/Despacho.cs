using System;
using System.Collections.Generic;

namespace backend_trazabilidad.Models;

/// <summary>
/// La Distribuidora declara la Factura y Guía de Despacho en unidades de garrafas de 10 kg, consumiendo el saldo del Lote envasado vinculado.
/// </summary>
public partial class Despacho
{
    /// <summary>
    /// identificador único
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// numero correlativo
    /// </summary>
    public int Correlativo { get; set; }

    /// <summary>
    /// fecha que se despacho el glp
    /// </summary>
    public DateTime FechaDespacho { get; set; }

    /// <summary>
    /// nombre del operador 
    /// </summary>
    public string Operador { get; set; } = null!;

    /// <summary>
    /// zona de distribucion
    /// </summary>
    public string? Zona { get; set; }

    /// <summary>
    /// camion distribuidor
    /// </summary>
    public string? CamionDistr { get; set; }

    /// <summary>
    /// numero de factura
    /// </summary>
    public string? NumFactura { get; set; }

    /// <summary>
    /// numero de garrafas despachadas
    /// </summary>
    public int? NumDespacho { get; set; }

    /// <summary>
    /// volumen equivalente
    /// </summary>
    public int? VolEquivalente { get; set; }

    /// <summary>
    /// unidad de medida del volumen equivalente 
    /// </summary>
    public string? UnidadEquivalente { get; set; }

    /// <summary>
    /// nombre del distribuidor
    /// </summary>
    public string? Distribuidor { get; set; }

    /// <summary>
    /// placa del distribuidor
    /// </summary>
    public string? PlacaDistribuidor { get; set; }

    /// <summary>
    /// observacion
    /// </summary>
    public string? Observacion { get; set; }

    /// <summary>
    /// estado del requistro
    /// </summary>
    public bool Estado { get; set; }

    /// <summary>
    /// usario de creacion del registro
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

    public virtual TblTrazabilidad? TblTrazabilidad { get; set; }
}
