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

    public virtual DbSet<TbCertificado> TbCertificados { get; set; }

    public virtual DbSet<TbCisterna> TbCisternas { get; set; }

    public virtual DbSet<TbDistribuidor> TbDistribuidors { get; set; }

    public virtual DbSet<TbEngarrafadora> TbEngarrafadoras { get; set; }

    public virtual DbSet<TbEvento> TbEventos { get; set; }

    public virtual DbSet<TbEventoComposicion> TbEventoComposicions { get; set; }

    public virtual DbSet<TbEventoDestino> TbEventoDestinos { get; set; }

    public virtual DbSet<TbEventoOrigen> TbEventoOrigens { get; set; }

    public virtual DbSet<TbInstancium> TbInstancia { get; set; }

    public virtual DbSet<TbLoteGlp> TbLoteGlps { get; set; }

    public virtual DbSet<TbPlantum> TbPlanta { get; set; }

    public virtual DbSet<TbTanque> TbTanques { get; set; }

    public virtual DbSet<TbTipoLugar> TbTipoLugars { get; set; }

    public virtual DbSet<TrazabilidadView> TrazabilidadViews { get; set; }

    public virtual DbSet<TblParametrica> TblParametricas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TbCertificado>(entity =>
        {
            entity.HasKey(e => e.IdCertificado).HasName("tb_certificado_pkey");

            entity.ToTable("tb_certificado", "trazabilidad_op", tb => tb.HasComment("Certificados asociados a cada evento/etapa de la trazabilidad."));

            entity.HasIndex(e => e.IdEvento, "idx_certificado_evento");

            entity.HasIndex(e => e.NumeroCertificado, "tb_certificado_numero_certificado_key").IsUnique();

            entity.Property(e => e.IdCertificado).HasColumnName("id_certificado");
            entity.Property(e => e.DocumentoUrl)
                .HasMaxLength(500)
                .HasColumnName("documento_url");
            entity.Property(e => e.Estado)
                .HasDefaultValue(true)
                .HasColumnName("estado");
            entity.Property(e => e.Feccre)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp(0) without time zone")
                .HasColumnName("feccre");
            entity.Property(e => e.FechaEmision)
                .HasColumnType("timestamp(0) without time zone")
                .HasColumnName("fecha_emision");
            entity.Property(e => e.FechaMuestreo)
                .HasColumnType("timestamp(0) without time zone")
                .HasColumnName("fecha_muestreo");
            entity.Property(e => e.HashDocumento)
                .HasMaxLength(128)
                .HasColumnName("hash_documento");
            entity.Property(e => e.IdEvento).HasColumnName("id_evento");
            entity.Property(e => e.Laboratorio)
                .HasMaxLength(200)
                .HasColumnName("laboratorio");
            entity.Property(e => e.NumeroCertificado)
                .HasMaxLength(100)
                .HasColumnName("numero_certificado");
            entity.Property(e => e.Resultado)
                .HasMaxLength(100)
                .HasColumnName("resultado");
            entity.Property(e => e.TipoCertificado)
                .HasMaxLength(50)
                .HasDefaultValueSql("'CALIDAD'::character varying")
                .HasColumnName("tipo_certificado");
            entity.Property(e => e.Usucre).HasColumnName("usucre");
            entity.Property(e => e.VolumenMuestreado)
                .HasPrecision(14, 3)
                .HasColumnName("volumen_muestreado");

            entity.HasOne(d => d.IdEventoNavigation).WithMany(p => p.TbCertificados)
                .HasForeignKey(d => d.IdEvento)
                .HasConstraintName("fk_certificado_evento");
        });

        modelBuilder.Entity<TbCisterna>(entity =>
        {
            entity.HasKey(e => e.IdCisterna).HasName("tb_cisterna_pkey");

            entity.ToTable("tb_cisterna", "trazabilidad_op", tb => tb.HasComment("Unidades de transporte de GLP."));

            entity.HasIndex(e => e.IdInstancia, "tb_cisterna_id_instancia_key").IsUnique();

            entity.HasIndex(e => e.Placa, "tb_cisterna_placa_key").IsUnique();

            entity.Property(e => e.IdCisterna).HasColumnName("id_cisterna");
            entity.Property(e => e.Activo).HasColumnName("activo");
            entity.Property(e => e.ActualizadoEn)
                .HasColumnType("timestamp(0) without time zone")
                .HasColumnName("actualizado_en");
            entity.Property(e => e.ActualizadoPor)
                .HasMaxLength(200)
                .HasColumnName("actualizado_por");
            entity.Property(e => e.Conductor)
                .HasMaxLength(150)
                .HasColumnName("conductor");
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
                .HasDefaultValue(true)
                .HasColumnName("estado");
            entity.Property(e => e.Feccre)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp(0) without time zone")
                .HasColumnName("feccre");
            entity.Property(e => e.IdActualizadoPor).HasColumnName("id_actualizado_por");
            entity.Property(e => e.IdCreadoPor).HasColumnName("id_creado_por");
            entity.Property(e => e.IdEliminadoPor).HasColumnName("id_eliminado_por");
            entity.Property(e => e.IdInstancia).HasColumnName("id_instancia");
            entity.Property(e => e.Matadata)
                .HasColumnType("jsonb")
                .HasColumnName("matadata");
            entity.Property(e => e.NroPrecinto)
                .HasMaxLength(100)
                .HasColumnName("nro_precinto");
            entity.Property(e => e.Observacion).HasColumnName("observacion");
            entity.Property(e => e.Placa)
                .HasMaxLength(20)
                .HasColumnName("placa");
            entity.Property(e => e.Usucre).HasColumnName("usucre");
            entity.Property(e => e.VolTotal)
                .HasPrecision(14, 3)
                .HasColumnName("vol_total");

            entity.HasOne(d => d.IdInstanciaNavigation).WithOne(p => p.TbCisterna)
                .HasForeignKey<TbCisterna>(d => d.IdInstancia)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_cisterna_instancia");
        });

        modelBuilder.Entity<TbDistribuidor>(entity =>
        {
            entity.HasKey(e => e.IdDistribuidor).HasName("tb_distribuidor_pkey");

            entity.ToTable("tb_distribuidor", "trazabilidad_op", tb => tb.HasComment("Distribuidores o destinos de la última etapa de la cadena."));

            entity.HasIndex(e => e.IdInstancia, "tb_distribuidor_id_instancia_key").IsUnique();

            entity.Property(e => e.IdDistribuidor).HasColumnName("id_distribuidor");
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
            entity.Property(e => e.EliminadoEn)
                .HasColumnType("timestamp(0) without time zone")
                .HasColumnName("eliminado_en");
            entity.Property(e => e.EliminadoPor)
                .HasMaxLength(200)
                .HasColumnName("eliminado_por");
            entity.Property(e => e.Estado)
                .HasDefaultValue(true)
                .HasColumnName("estado");
            entity.Property(e => e.IdActualizadoPor).HasColumnName("id_actualizado_por");
            entity.Property(e => e.IdCreadoPor).HasColumnName("id_creado_por");
            entity.Property(e => e.IdEliminadoPor).HasColumnName("id_eliminado_por");
            entity.Property(e => e.IdInstancia).HasColumnName("id_instancia");
            entity.Property(e => e.Matadata)
                .HasColumnType("jsonb")
                .HasColumnName("matadata");
            entity.Property(e => e.NroFactura)
                .HasMaxLength(255)
                .HasColumnName("nro_factura");
            entity.Property(e => e.NroGarrafas).HasColumnName("nro_garrafas");
            entity.Property(e => e.Observacin).HasColumnName("observacin");
            entity.Property(e => e.PlacaDistribuidor)
                .HasMaxLength(255)
                .HasColumnName("placa_distribuidor");
            entity.Property(e => e.Ubicacion)
                .HasMaxLength(250)
                .HasColumnName("ubicacion");
            entity.Property(e => e.VolEquivalente).HasColumnName("vol_equivalente");
            entity.Property(e => e.ZonaDistribucion).HasColumnName("zona_distribucion");

            entity.HasOne(d => d.IdInstanciaNavigation).WithOne(p => p.TbDistribuidor)
                .HasForeignKey<TbDistribuidor>(d => d.IdInstancia)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_distribuidor_instancia");
        });

        modelBuilder.Entity<TbEngarrafadora>(entity =>
        {
            entity.HasKey(e => e.IdEngarrafadora).HasName("tb_engarrafadora_pkey");

            entity.ToTable("tb_engarrafadora", "trazabilidad_op", tb => tb.HasComment("Instalaciones de engarrafado que reciben GLP desde uno o varios tanques."));

            entity.HasIndex(e => e.IdInstancia, "tb_engarrafadora_id_instancia_key").IsUnique();

            entity.Property(e => e.IdEngarrafadora).HasColumnName("id_engarrafadora");
            entity.Property(e => e.Activo).HasColumnName("activo");
            entity.Property(e => e.ActualizadoEn)
                .HasColumnType("timestamp(0) without time zone")
                .HasColumnName("actualizado_en");
            entity.Property(e => e.ActualizadoPor)
                .HasMaxLength(200)
                .HasColumnName("actualizado_por");
            entity.Property(e => e.CapacidadDiaria)
                .HasPrecision(14, 3)
                .HasColumnName("capacidad_diaria");
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
                .HasDefaultValue(true)
                .HasColumnName("estado");
            entity.Property(e => e.Feccre)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp(0) without time zone")
                .HasColumnName("feccre");
            entity.Property(e => e.IdActualizadoPor).HasColumnName("id_actualizado_por");
            entity.Property(e => e.IdCreadoPor).HasColumnName("id_creado_por");
            entity.Property(e => e.IdEliminadoPor).HasColumnName("id_eliminado_por");
            entity.Property(e => e.IdInstancia).HasColumnName("id_instancia");
            entity.Property(e => e.Matadata)
                .HasColumnType("jsonb")
                .HasColumnName("matadata");
            entity.Property(e => e.Observacion).HasColumnName("observacion");
            entity.Property(e => e.Usucre).HasColumnName("usucre");
            entity.Property(e => e.VolDisponible).HasColumnName("vol_disponible");
            entity.Property(e => e.VolEnvasado).HasColumnName("vol_envasado");

            entity.HasOne(d => d.IdInstanciaNavigation).WithOne(p => p.TbEngarrafadora)
                .HasForeignKey<TbEngarrafadora>(d => d.IdInstancia)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_engarrafadora_instancia");
        });

        modelBuilder.Entity<TbEvento>(entity =>
        {
            entity.HasKey(e => e.IdEvento).HasName("tb_evento_pkey");

            entity.ToTable("tb_evento", "trazabilidad_op", tb => tb.HasComment("Hecho ocurrido con el GLP. Agrupa sus orígenes, destinos y certificados."));

            entity.HasIndex(e => new { e.TipoEvento, e.FechaEvento }, "idx_evento_tipo_fecha");

            entity.Property(e => e.IdEvento).HasColumnName("id_evento");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(500)
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .HasDefaultValueSql("'CONFIRMADO'::character varying")
                .HasColumnName("estado");
            entity.Property(e => e.Feccre)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp(0) without time zone")
                .HasColumnName("feccre");
            entity.Property(e => e.FechaEvento)
                .HasColumnType("timestamp(0) without time zone")
                .HasColumnName("fecha_evento");
            entity.Property(e => e.Observacion).HasColumnName("observacion");
            entity.Property(e => e.TipoEvento)
                .HasMaxLength(30)
                .HasColumnName("tipo_evento");
            entity.Property(e => e.Usucre).HasColumnName("usucre");
        });

        modelBuilder.Entity<TbEventoComposicion>(entity =>
        {
            entity.HasKey(e => e.IdComposicion).HasName("tb_evento_composicion_pkey");

            entity.ToTable("tb_evento_composicion", "trazabilidad_op", tb => tb.HasComment("Genealogía de mezclas: indica cuánto de cada lote compone un destino."));

            entity.HasIndex(e => e.IdLoteOrigen, "idx_composicion_lote");

            entity.HasIndex(e => new { e.IdEventoDestino, e.IdLoteOrigen }, "uq_composicion").IsUnique();

            entity.Property(e => e.IdComposicion).HasColumnName("id_composicion");
            entity.Property(e => e.IdEventoDestino).HasColumnName("id_evento_destino");
            entity.Property(e => e.IdLoteOrigen).HasColumnName("id_lote_origen");
            entity.Property(e => e.Porcentaje)
                .HasPrecision(8, 5)
                .HasColumnName("porcentaje");
            entity.Property(e => e.Volumen)
                .HasPrecision(14, 3)
                .HasColumnName("volumen");

            entity.HasOne(d => d.IdEventoDestinoNavigation).WithMany(p => p.TbEventoComposicions)
                .HasForeignKey(d => d.IdEventoDestino)
                .HasConstraintName("fk_composicion_destino");

            entity.HasOne(d => d.IdLoteOrigenNavigation).WithMany(p => p.TbEventoComposicions)
                .HasForeignKey(d => d.IdLoteOrigen)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_composicion_lote");
        });

        modelBuilder.Entity<TbEventoDestino>(entity =>
        {
            entity.HasKey(e => e.IdEventoDestino).HasName("tb_evento_destino_pkey");

            entity.ToTable("tb_evento_destino", "trazabilidad_op", tb => tb.HasComment("Detalle de destinos. Permite múltiples destinos en un evento, por ejemplo un tanque hacia 3 engarrafadoras."));

            entity.HasIndex(e => e.IdInstancia, "idx_evento_destino_instancia");

            entity.HasIndex(e => e.IdLote, "idx_evento_destino_lote");

            entity.HasIndex(e => new { e.IdEvento, e.IdInstancia, e.IdLote }, "uq_evento_destino").IsUnique();

            entity.Property(e => e.IdEventoDestino).HasColumnName("id_evento_destino");
            entity.Property(e => e.IdEvento).HasColumnName("id_evento");
            entity.Property(e => e.IdInstancia).HasColumnName("id_instancia");
            entity.Property(e => e.IdLote).HasColumnName("id_lote");
            entity.Property(e => e.Porcentaje)
                .HasPrecision(8, 5)
                .HasColumnName("porcentaje");
            entity.Property(e => e.Volumen)
                .HasPrecision(14, 3)
                .HasColumnName("volumen");

            entity.HasOne(d => d.IdEventoNavigation).WithMany(p => p.TbEventoDestinos)
                .HasForeignKey(d => d.IdEvento)
                .HasConstraintName("fk_evento_destino_evento");

            entity.HasOne(d => d.IdInstanciaNavigation).WithMany(p => p.TbEventoDestinos)
                .HasForeignKey(d => d.IdInstancia)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_evento_destino_instancia");

            entity.HasOne(d => d.IdLoteNavigation).WithMany(p => p.TbEventoDestinos)
                .HasForeignKey(d => d.IdLote)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_evento_destino_lote");
        });

        modelBuilder.Entity<TbEventoOrigen>(entity =>
        {
            entity.HasKey(e => e.IdEventoOrigen).HasName("tb_evento_origen_pkey");

            entity.ToTable("tb_evento_origen", "trazabilidad_op", tb => tb.HasComment("Detalle de orígenes. Permite múltiples orígenes en un mismo evento, por ejemplo 3 cisternas hacia un tanque."));

            entity.HasIndex(e => e.IdInstancia, "idx_evento_origen_instancia");

            entity.HasIndex(e => e.IdLote, "idx_evento_origen_lote");

            entity.HasIndex(e => new { e.IdEvento, e.IdInstancia, e.IdLote }, "uq_evento_origen").IsUnique();

            entity.Property(e => e.IdEventoOrigen).HasColumnName("id_evento_origen");
            entity.Property(e => e.IdEvento).HasColumnName("id_evento");
            entity.Property(e => e.IdInstancia).HasColumnName("id_instancia");
            entity.Property(e => e.IdLote).HasColumnName("id_lote");
            entity.Property(e => e.Observacion).HasColumnName("observacion");
            entity.Property(e => e.Porcentaje)
                .HasPrecision(8, 5)
                .HasColumnName("porcentaje");
            entity.Property(e => e.Volumen)
                .HasPrecision(14, 3)
                .HasColumnName("volumen");

            entity.HasOne(d => d.IdEventoNavigation).WithMany(p => p.TbEventoOrigens)
                .HasForeignKey(d => d.IdEvento)
                .HasConstraintName("fk_evento_origen_evento");

            entity.HasOne(d => d.IdInstanciaNavigation).WithMany(p => p.TbEventoOrigens)
                .HasForeignKey(d => d.IdInstancia)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_evento_origen_instancia");

            entity.HasOne(d => d.IdLoteNavigation).WithMany(p => p.TbEventoOrigens)
                .HasForeignKey(d => d.IdLote)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_evento_origen_lote");
        });

        modelBuilder.Entity<TbInstancium>(entity =>
        {
            entity.HasKey(e => e.IdInstancia).HasName("tb_instancia_pkey");

            entity.ToTable("tb_instancia", "trazabilidad_op", tb => tb.HasComment("Superentidad que asigna un ID universal a cada planta, cisterna, tanque, engarrafadora o distribuidor. Este id_instancia es el ID de origen/destino usado por los eventos."));

            entity.HasIndex(e => e.IdTipoLugar, "idx_instancia_tipo");

            entity.HasIndex(e => new { e.IdTipoLugar, e.Codigo }, "uq_instancia_tipo_codigo").IsUnique();

            entity.Property(e => e.IdInstancia).HasColumnName("id_instancia");
            entity.Property(e => e.Codigo)
                .HasMaxLength(50)
                .HasColumnName("codigo");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(250)
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado)
                .HasDefaultValue(true)
                .HasColumnName("estado");
            entity.Property(e => e.Feccre)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp(0) without time zone")
                .HasColumnName("feccre");
            entity.Property(e => e.Fecmod)
                .HasColumnType("timestamp(0) without time zone")
                .HasColumnName("fecmod");
            entity.Property(e => e.IdTipoLugar).HasColumnName("id_tipo_lugar");
            entity.Property(e => e.Nombre)
                .HasMaxLength(200)
                .HasColumnName("nombre");
            entity.Property(e => e.Ubicacion)
                .HasMaxLength(250)
                .HasColumnName("ubicacion");
            entity.Property(e => e.Usucre).HasColumnName("usucre");
            entity.Property(e => e.Usumod).HasColumnName("usumod");

            entity.HasOne(d => d.IdTipoLugarNavigation).WithMany(p => p.TbInstancia)
                .HasForeignKey(d => d.IdTipoLugar)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_instancia_tipo");
        });

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
            entity.HasOne(d => d.PlantaOrigenNavigation)
                .WithMany(p => p.TbLoteGlps)
                .HasForeignKey(d => d.IdPlantaOrigen)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<TbPlantum>(entity =>
        {
            entity.HasKey(e => e.IdPlanta).HasName("tb_planta_pkey");

            entity.ToTable("tb_planta", "trazabilidad_op", tb => tb.HasComment("Datos propios de una planta local o importadora."));

            entity.HasIndex(e => e.IdInstancia, "tb_planta_id_instancia_key").IsUnique();

            entity.Property(e => e.IdPlanta).HasColumnName("id_planta");
            entity.Property(e => e.Departamento)
                .HasMaxLength(100)
                .HasColumnName("departamento");
            entity.Property(e => e.EliminadoPor)
                .HasMaxLength(200)
                .HasColumnName("eliminado_por");
            entity.Property(e => e.Estado)
                .HasDefaultValue(true)
                .HasColumnName("estado");
            entity.Property(e => e.IdInstancia).HasColumnName("id_instancia");
            entity.Property(e => e.Observacion).HasColumnName("observacion");
            entity.Property(e => e.Pais)
                .HasMaxLength(100)
                .HasColumnName("pais");
            entity.Property(e => e.PuntoIngreso)
                .HasMaxLength(200)
                .HasColumnName("punto_ingreso");
            entity.Property(e => e.TipoOperacion)
                .HasComment("1=Planta local; 2=Importación.")
                .HasColumnName("tipo_operacion");

            entity.HasOne(d => d.IdInstanciaNavigation).WithOne(p => p.TbPlantum)
                .HasForeignKey<TbPlantum>(d => d.IdInstancia)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_planta_instancia");
        });

        modelBuilder.Entity<TbTanque>(entity =>
        {
            entity.HasKey(e => e.IdTanque).HasName("tb_tanque_pkey");

            entity.ToTable("tb_tanque", "trazabilidad_op", tb => tb.HasComment("Tanques físicos. Pueden recibir GLP de varias cisternas y alimentar varias engarrafadoras."));

            entity.HasIndex(e => e.IdPlanta, "idx_tanque_planta");

            entity.HasIndex(e => e.IdInstancia, "tb_tanque_id_instancia_key").IsUnique();

            entity.Property(e => e.IdTanque).HasColumnName("id_tanque");
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
            entity.Property(e => e.EliminadoEn)
                .HasColumnType("timestamp(0) without time zone")
                .HasColumnName("eliminado_en");
            entity.Property(e => e.EliminadoPor)
                .HasMaxLength(200)
                .HasColumnName("eliminado_por");
            entity.Property(e => e.Estado)
                .HasDefaultValue(true)
                .HasColumnName("estado");
            entity.Property(e => e.IdActualizadoPor).HasColumnName("id_actualizado_por");
            entity.Property(e => e.IdCreadoPor).HasColumnName("id_creado_por");
            entity.Property(e => e.IdEliminadoPor).HasColumnName("id_eliminado_por");
            entity.Property(e => e.IdInstancia).HasColumnName("id_instancia");
            entity.Property(e => e.IdPlanta).HasColumnName("id_planta");
            entity.Property(e => e.Matadata)
                .HasColumnType("jsonb")
                .HasColumnName("matadata");
            entity.Property(e => e.Observacion).HasColumnName("observacion");
            entity.Property(e => e.VolTotal)
                .HasPrecision(14, 3)
                .HasColumnName("vol_total");

            entity.HasOne(d => d.IdInstanciaNavigation).WithOne(p => p.TbTanque)
                .HasForeignKey<TbTanque>(d => d.IdInstancia)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_tanque_instancia");

            entity.HasOne(d => d.IdPlantaNavigation).WithMany(p => p.TbTanques)
                .HasForeignKey(d => d.IdPlanta)
                .HasConstraintName("fk_tanque_planta");
        });

        modelBuilder.Entity<TbTipoLugar>(entity =>
        {
            entity.HasKey(e => e.IdTipoLugar).HasName("tb_tipo_lugar_pkey");

            entity.ToTable("tb_tipo_lugar", "trazabilidad_op", tb => tb.HasComment("Catálogo de tipos de nodo: planta, transporte, almacenamiento, engarrafado y distribución."));

            entity.HasIndex(e => e.Codigo, "tb_tipo_lugar_codigo_key").IsUnique();

            entity.Property(e => e.IdTipoLugar).HasColumnName("id_tipo_lugar");
            entity.Property(e => e.Codigo)
                .HasMaxLength(3)
                .HasColumnName("codigo");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(250)
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado)
                .HasDefaultValue(true)
                .HasColumnName("estado");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
        });

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

        modelBuilder.Entity<TblParametrica>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("tbl_parametrica_pk");

            entity.ToTable("tbl_parametrica", "traz_parametrica");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Categoria)
                .HasMaxLength(255)
                .HasColumnName("categoria");
            entity.Property(e => e.Descripcion).HasColumnName("descripcion");
            entity.Property(e => e.Estado)
                .HasDefaultValue(true)
                .HasColumnName("estado");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");
            entity.Property(e => e.Valor)
                .HasMaxLength(255)
                .HasColumnName("valor");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
