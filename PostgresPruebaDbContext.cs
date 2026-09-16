using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using backend_trazabilidad.Models.Postgresql;

namespace backend_trazabilidad;

public partial class PostgresPruebaDbContext : DbContext
{
    public PostgresPruebaDbContext(DbContextOptions<PostgresPruebaDbContext> options)
        : base(options)
    {
    }

    
}
