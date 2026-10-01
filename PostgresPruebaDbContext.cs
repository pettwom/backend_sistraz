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

    public virtual DbSet<TbCertificado> TbCertificados { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TbCertificado>(entity =>
        {
            entity.HasKey(e => e.IdCertificado).HasName("tb_certificado_pkey");

            entity.ToTable("tb_certificado", "trazabilidad_op");

            entity.HasIndex(e => e.IdEvento, "idx_certificado_evento");

            entity.HasIndex(e => e.NumeroCertificado, "tb_certificado_numero_certificado_key").IsUnique();

            entity.Property(e => e.IdCertificado).HasColumnName("id_certificado");
            entity.Property(e => e.Activo).HasColumnName("activo");
            entity.Property(e => e.ActualizadoEn)
                .HasColumnType("timestamp(0) without time zone")
                .HasColumnName("actualizado_en");
            entity.Property(e => e.ActualizadoPor)
                .HasMaxLength(200)
                .HasColumnName("actualizado_por");
            entity.Property(e => e.CreadoEn)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp(0) without time zone")
                .HasColumnName("creado_en");
            entity.Property(e => e.CreadoPor)
                .HasMaxLength(200)
                .HasColumnName("creado_por");
            entity.Property(e => e.DocumentoUrl)
                .HasMaxLength(500)
                .HasColumnName("documento_url");
            entity.Property(e => e.EliminadoEn)
                .HasColumnType("timestamp(0) without time zone")
                .HasColumnName("eliminado_en");
            entity.Property(e => e.EliminadoPor)
                .HasMaxLength(200)
                .HasColumnName("eliminado_por");
            entity.Property(e => e.Estado)
                .HasDefaultValue(true)
                .HasColumnName("estado");
            entity.Property(e => e.FechaMuestreo)
                .HasColumnType("timestamp(0) without time zone")
                .HasColumnName("fecha_muestreo");
            entity.Property(e => e.IdActualizadoPor).HasColumnName("id_actualizado_por");
            entity.Property(e => e.IdCertParam).HasColumnName("id_cert_param");
            entity.Property(e => e.IdCreadoPor).HasColumnName("id_creado_por");
            entity.Property(e => e.IdEliminadoPor).HasColumnName("id_eliminado_por");
            entity.Property(e => e.IdEvento).HasColumnName("id_evento");
            entity.Property(e => e.IdInstancia).HasColumnName("id_instancia");
            entity.Property(e => e.Matadata)
                .HasColumnType("jsonb")
                .HasColumnName("matadata");
            entity.Property(e => e.NumeroCertificado)
                .HasMaxLength(100)
                .HasColumnName("numero_certificado");
            entity.Property(e => e.Observacion).HasColumnName("observacion");
            entity.Property(e => e.Tag)
                .HasMaxLength(50)
                .HasColumnName("tag");
            entity.Property(e => e.TamCert)
                .HasMaxLength(128)
                .HasColumnName("tam_cert");
            entity.Property(e => e.TipoCertificado)
                .HasMaxLength(50)
                .HasDefaultValueSql("'CALIDAD'::character varying")
                .HasColumnName("tipo_certificado");
            entity.Property(e => e.UnidadMed)
                .HasMaxLength(100)
                .HasColumnName("unidad_med");
            entity.Property(e => e.VolumenLote)
                .HasPrecision(14, 3)
                .HasColumnName("volumen_lote");
        });
        modelBuilder.HasSequence("seq_idproduccion", "traz_operativo");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
