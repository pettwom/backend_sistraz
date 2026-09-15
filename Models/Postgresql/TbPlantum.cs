using System;
using System.Collections.Generic;

namespace backend_trazabilidad.Models.Postgresql;

/// <summary>
/// Datos propios de una planta local o importadora.
/// </summary>
public partial class TbPlantum
{
    public long IdPlanta { get; set; }

    public long IdInstancia { get; set; }

    /// <summary>
    /// 1=Planta local; 2=Importación.
    /// </summary>
    public short TipoOperacion { get; set; }

    public string? Pais { get; set; }

    public string? Departamento { get; set; }

    public string? PuntoIngreso { get; set; }

    public bool Estado { get; set; }

    public string? Observacion { get; set; }

    public string? EliminadoPor { get; set; }

    public virtual TbInstancium IdInstanciaNavigation { get; set; } = null!;

    public virtual ICollection<TbLoteGlp> TbLoteGlps { get; set; } = new List<TbLoteGlp>();

    public virtual ICollection<TbTanque> TbTanques { get; set; } = new List<TbTanque>();
}
