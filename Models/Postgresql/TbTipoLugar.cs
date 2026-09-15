using System;
using System.Collections.Generic;

namespace backend_trazabilidad.Models.Postgresql;

/// <summary>
/// Catálogo de tipos de nodo: planta, transporte, almacenamiento, engarrafado y distribución.
/// </summary>
public partial class TbTipoLugar
{
    public short IdTipoLugar { get; set; }

    public string Codigo { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public bool Estado { get; set; }

    public virtual ICollection<TbInstancium> TbInstancia { get; set; } = new List<TbInstancium>();
}
