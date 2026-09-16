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

    public virtual DbSet<TbLoteGlp> TbLoteGlps { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TbLoteGlp>(entity =>
        {
            entity.HasKey(e => e.IdLote).HasName("tb_lote_glp_pkey");

            entity.ToTable("tb_lote_glp", "trazabilidad_op", tb => tb.HasComment("Identidad del GLP trazable. El código del lote se mantiene a través de los eventos."));

            entity.HasIndex(e => e.FechaOrigen, "idx_lote_fecha");

            entity.HasIndex(e => e.IdPlantaOrigen, "idx_lote_planta");

            entity.HasIndex(e => e.Codigo, "tb_lote_glp_codigo_key").IsUnique();

            entity.Property(e => e.IdLote).HasColumnName("id_lote");
            entity.Property(e => e.Activo).HasColumnName("activo");
            entity.Property(e => e.ActualizadoEn)
                .HasColumnType("timestamp(0) without time zone")
                .HasColumnName("actualizado_en");
            entity.Property(e => e.ActualizadoPor)
                .HasMaxLength(200)
                .HasColumnName("actualizado_por");
            entity.Property(e => e.Codigo)
                .HasMaxLength(60)
                .HasColumnName("codigo");
            entity.Property(e => e.CreadoEn)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp(0) without time zone")
                .HasColumnName("creado_en");
            entity.Property(e => e.CreadoPor)
                .HasMaxLength(200)
                .HasColumnName("creado_por");
            entity.Property(e => e.EliminadoEn)
                .HasColumnType("timestamp(0) without time zone")
                .HasColumnName("eliminado_en");
            entity.Property(e => e.EliminadoPor)
                .HasMaxLength(200)
                .HasColumnName("eliminado_por");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .HasDefaultValueSql("'ACTIVO'::character varying")
                .HasColumnName("estado");
            entity.Property(e => e.FechaOrigen)
                .HasColumnType("timestamp(0) without time zone")
                .HasColumnName("fecha_origen");
            entity.Property(e => e.IdActualizadoPor).HasColumnName("id_actualizado_por");
            entity.Property(e => e.IdCreadoPor).HasColumnName("id_creado_por");
            entity.Property(e => e.IdEliminadoPor).HasColumnName("id_eliminado_por");
            entity.Property(e => e.IdPlantaOrigen).HasColumnName("id_planta_origen");
            entity.Property(e => e.Matadata)
                .HasColumnType("jsonb")
                .HasColumnName("matadata");
            entity.Property(e => e.Unidad)
                .HasMaxLength(10)
                .HasDefaultValueSql("'TN'::character varying")
                .HasColumnName("unidad");
            entity.Property(e => e.VolumenInicial)
                .HasPrecision(14, 3)
                .HasColumnName("volumen_inicial");
        });
        modelBuilder.HasSequence("seq_idproduccion", "traz_operativo");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
