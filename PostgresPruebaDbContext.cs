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

    public virtual DbSet<TrazabilidadView> TrazabilidadViews { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TrazabilidadView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("trazabilidad_view", "trazabilidad_op");

            entity.Property(e => e.Certificados)
                .HasColumnType("jsonb")
                .HasColumnName("certificados");
            entity.Property(e => e.CodigoTrazabilidad)
                .HasMaxLength(60)
                .HasColumnName("codigo_trazabilidad");
            entity.Property(e => e.Destinos)
                .HasColumnType("jsonb")
                .HasColumnName("destinos");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .HasColumnName("estado");
            entity.Property(e => e.FechaEvento)
                .HasColumnType("timestamp(0) without time zone")
                .HasColumnName("fecha_evento");
            entity.Property(e => e.IdEvento).HasColumnName("id_evento");
            entity.Property(e => e.IdLote).HasColumnName("id_lote");
            entity.Property(e => e.Origenes)
                .HasColumnType("jsonb")
                .HasColumnName("origenes");
            entity.Property(e => e.TipoEvento)
                .HasMaxLength(30)
                .HasColumnName("tipo_evento");
        });
        modelBuilder.HasSequence("seq_idproduccion", "traz_operativo");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
