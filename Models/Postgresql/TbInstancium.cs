using System;
using System.Collections.Generic;

namespace backend_trazabilidad.Models.Postgresql;

public partial class TbInstancium
{
    public long IdInstancia { get; set; }

    public short IdTipoLugar { get; set; }

    public string Codigo { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public string? Ubicacion { get; set; }

    public bool Estado { get; set; }

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

    public virtual TbTipoLugar IdTipoLugarNavigation { get; set; } = null!;

    public virtual TbDistribuidor? TbDistribuidor { get; set; }

    public virtual TbEngarrafadora? TbEngarrafadora { get; set; }

    public virtual ICollection<TbEventoDestino> TbEventoDestinos { get; set; } = new List<TbEventoDestino>();

    public virtual ICollection<TbEventoOrigen> TbEventoOrigens { get; set; } = new List<TbEventoOrigen>();

    public virtual TbPlantum? TbPlantum { get; set; }

    public virtual TbTanque? TbTanque { get; set; }
}
