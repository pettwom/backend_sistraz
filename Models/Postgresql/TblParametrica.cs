using System;
using System.Collections.Generic;

namespace backend_trazabilidad.Models.Postgresql;

public partial class TblParametrica
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public string? Valor { get; set; }

    public bool Estado { get; set; }

    public string? Categoria { get; set; }
}
