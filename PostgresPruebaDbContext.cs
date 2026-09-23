using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using backend_trazabilidad.Models.PostgresqlPrueba;

namespace backend_trazabilidad;

public partial class PostgresPruebaDbContext : DbContext
{
    public PostgresPruebaDbContext(DbContextOptions<PostgresPruebaDbContext> options)
        : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

    }

}
