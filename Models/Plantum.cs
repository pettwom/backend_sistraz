using System;
using System.Collections.Generic;

namespace backend_trazabilidad.Models;

public partial class Plantum
{
    /// <summary>
    /// identificador unico
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// nombre de la planta o del operador
    /// </summary>
    public string Nombre { get; set; } = null!;

    /// <summary>
    /// tipo de operacion 1=planta local; 2=importacion
    /// </summary>
    public int TipoOp { get; set; }

    /// <summary>
    /// pais de importacion
    /// </summary>
    public string? Pais { get; set; }

    /// <summary>
    /// punto de ingreso al pais
    /// </summary>
    public string? PuntoIngreso { get; set; }

    /// <summary>
    /// volumen total expresado en Toneladas
    /// </summary>
    public int VolTotal { get; set; }

    public virtual Produccion IdNavigation { get; set; } = null!;
}
