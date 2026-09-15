using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using backend_trazabilidad.Models.Postgresql;

namespace backend_trazabilidad;

public partial class PostgresDbContext : DbContext
{
    public PostgresDbContext(DbContextOptions<PostgresDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Plantum> Planta { get; set; }

    public virtual DbSet<Produccion> Produccions { get; set; }

    public virtual DbSet<TblTrazabilidad> TblTrazabilidads { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Plantum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("planta_pk");

            entity.ToTable("planta", "traz_operativo");

            entity.Property(e => e.Id)
                .HasComment("identificador unico")
                .HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasComment("nombre de la planta o del operador")
                .HasColumnName("nombre");
            entity.Property(e => e.Pais)
                .HasMaxLength(100)
                .HasComment("pais de importacion")
                .HasColumnName("pais");
            entity.Property(e => e.PuntoIngreso)
                .HasMaxLength(100)
                .HasComment("punto de ingreso al pais")
                .HasColumnName("punto_ingreso");
            entity.Property(e => e.TipoOp)
                .HasDefaultValue(1)
                .HasComment("tipo de operacion 1=planta local; 2=importacion")
                .HasColumnName("tipo_op");
            entity.Property(e => e.VolTotal)
                .HasComment("volumen total expresado en Toneladas")
                .HasColumnName("vol_total");
        });

        modelBuilder.Entity<Produccion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("produccion_pk");

            entity.ToTable("produccion", "traz_operativo");

            entity.HasIndex(e => e.NumCertCal, "produccion_num_cert_cal_index");

            entity.Property(e => e.Id)
                .HasComment("identificador unico para produccion")
                .HasColumnName("id");
            entity.Property(e => e.Activo).HasColumnName("activo");
            entity.Property(e => e.ActualizadoEn)
                .HasColumnType("timestamp(0) without time zone")
                .HasColumnName("actualizado_en");
            entity.Property(e => e.ActualizadoPor)
                .HasMaxLength(200)
                .HasColumnName("actualizado_por");
            entity.Property(e => e.Correlativo)
                .HasComment("numero correlativo enlazado a tbl_trazabilidad")
                .HasColumnName("correlativo");
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
            entity.Property(e => e.FechaMuestreo)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_muestreo");
            entity.Property(e => e.IdActualizadoPor).HasColumnName("id_actualizado_por");
            entity.Property(e => e.IdCreadoPor).HasColumnName("id_creado_por");
            entity.Property(e => e.IdEliminadoPor).HasColumnName("id_eliminado_por");
            entity.Property(e => e.IdPlanta)
                .HasComment("Identificador de la planta o operador")
                .HasColumnName("id_planta");
            entity.Property(e => e.Metadata)
                .HasColumnType("jsonb")
                .HasColumnName("metadata");
            entity.Property(e => e.NumCertCal)
                .HasComment("numero de certificado de calidad")
                .HasColumnType("character varying")
                .HasColumnName("num_cert_cal");
            entity.Property(e => e.Observacion)
                .HasComment("observacion de la produccion")
                .HasColumnName("observacion");
        });

        modelBuilder.Entity<TblTrazabilidad>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("tbl_trazabilidad_pk");

            entity.ToTable("tbl_trazabilidad", "traz_operativo");

            entity.HasIndex(e => new { e.IdPlanta, e.CorrOri }, "tbl_trazabilidad_id_planta_corr_ori_index");

            entity.HasIndex(e => e.CorrDest, "tbl_trazabilidad_pk_2").IsUnique();

            entity.Property(e => e.Id)
                .HasComment("identificador unico")
                .HasColumnName("id");
            entity.Property(e => e.Activo).HasColumnName("activo");
            entity.Property(e => e.ActualizadoEn)
                .HasColumnType("timestamp(0) without time zone")
                .HasColumnName("actualizado_en");
            entity.Property(e => e.ActualizadoPor)
                .HasMaxLength(200)
                .HasColumnName("actualizado_por");
            entity.Property(e => e.CorrDest)
                .HasComment("correlativo destino")
                .HasColumnName("corr_dest");
            entity.Property(e => e.CorrOri)
                .HasComment("correlativo de origen")
                .HasColumnName("corr_ori");
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
            entity.Property(e => e.IdActualizadoPor).HasColumnName("id_actualizado_por");
            entity.Property(e => e.IdCreadoPor).HasColumnName("id_creado_por");
            entity.Property(e => e.IdEliminadoPor).HasColumnName("id_eliminado_por");
            entity.Property(e => e.IdPlanta).HasColumnName("id_planta");
            entity.Property(e => e.Lugar)
                .HasComment("lugar del flujo")
                .HasColumnType("character varying")
                .HasColumnName("lugar");
            entity.Property(e => e.MesAnio)
                .HasDefaultValueSql("(date_trunc('month'::text, (CURRENT_DATE)::timestamp with time zone))::date")
                .HasColumnType("character varying")
                .HasColumnName("mes-anio");
            entity.Property(e => e.Metadata)
                .HasColumnType("jsonb")
                .HasColumnName("metadata");
        });
        modelBuilder.HasSequence("seq_idproduccion", "traz_operativo");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
