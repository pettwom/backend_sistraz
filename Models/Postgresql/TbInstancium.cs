using System;
using System.Collections.Generic;

namespace backend_trazabilidad.Models.Postgresql;

/// <summary>
/// Superentidad que asigna un ID universal a cada planta, cisterna, tanque, engarrafadora o distribuidor. Este id_instancia es el ID de origen/destino usado por los eventos.
/// </summary>
public partial class TbInstancium
{
    public long IdInstancia { get; set; }

    public short IdTipoLugar { get; set; }

    public string Codigo { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public string? Ubicacion { get; set; }

    public bool Estado { get; set; }

    public long Usucre { get; set; }

    public DateTime Feccre { get; set; }

    public long? Usumod { get; set; }

    public DateTime? Fecmod { get; set; }

    public virtual TbTipoLugar IdTipoLugarNavigation { get; set; } = null!;

    public virtual TbCisterna? TbCisterna { get; set; }

    public virtual TbDistribuidor? TbDistribuidor { get; set; }

    public virtual TbEngarrafadora? TbEngarrafadora { get; set; }

    public virtual ICollection<TbEventoDestino> TbEventoDestinos { get; set; } = new List<TbEventoDestino>();

    public virtual ICollection<TbEventoOrigen> TbEventoOrigens { get; set; } = new List<TbEventoOrigen>();

    public virtual TbPlantum? TbPlantum { get; set; }

    public virtual TbTanque? TbTanque { get; set; }
}
