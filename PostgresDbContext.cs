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

    public virtual DbSet<Almacenado> Almacenados { get; set; }

    public virtual DbSet<Despacho> Despachos { get; set; }

    public virtual DbSet<Envasado> Envasados { get; set; }

    public virtual DbSet<Plantum> Planta { get; set; }

    public virtual DbSet<Produccion> Produccions { get; set; }

    public virtual DbSet<Tanque> Tanques { get; set; }

    public virtual DbSet<TbCertDetalle> TbCertDetalles { get; set; }

    public virtual DbSet<TbCertEspec> TbCertEspecs { get; set; }

    public virtual DbSet<TbCertificado> TbCertificados { get; set; }

    public virtual DbSet<TbCisterna> TbCisternas { get; set; }

    public virtual DbSet<TbCisternaDetalle> TbCisternaDetalles { get; set; }

    public virtual DbSet<TbDistribuidor> TbDistribuidors { get; set; }

    public virtual DbSet<TbEngarrafadora> TbEngarrafadoras { get; set; }

    public virtual DbSet<TbEvento> TbEventos { get; set; }

    public virtual DbSet<TbEventoComposicion> TbEventoComposicions { get; set; }

    public virtual DbSet<TbEventoDestino> TbEventoDestinos { get; set; }

    public virtual DbSet<TbEventoOrigen> TbEventoOrigens { get; set; }

    public virtual DbSet<TbInstancium> TbInstancia { get; set; }

    public virtual DbSet<TbLoteGlp> TbLoteGlps { get; set; }

    public virtual DbSet<TbParamCert> TbParamCerts { get; set; }

    public virtual DbSet<TbPlantum> TbPlanta { get; set; }

    public virtual DbSet<TbTanque> TbTanques { get; set; }

    public virtual DbSet<TbTipoLugar> TbTipoLugars { get; set; }

    public virtual DbSet<TblParametrica> TblParametricas { get; set; }

    public virtual DbSet<TblTrazabilidad> TblTrazabilidads { get; set; }

    public virtual DbSet<TblTrazabilidad1> TblTrazabilidads1 { get; set; }

    public virtual DbSet<Transporte> Transportes { get; set; }

    public virtual DbSet<TrazabilidadView> TrazabilidadViews { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Almacenado>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("almcenado_pk");

            entity.ToTable("almacenado", "traz_operativo");

            entity.HasIndex(e => new { e.Correlativo, e.NombreOperador, e.NumCert }, "almcenado_correlativo_nombre_operador_num_cert_index");

            entity.HasIndex(e => e.Correlativo, "unq_almacenado_correlativo").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nextval('traz_operativo.almcenado_id_seq'::regclass)")
                .HasComment("identificador unico")
                .HasColumnName("id");
            entity.Property(e => e.Correlativo)
                .HasComment("correlativo")
                .HasColumnName("correlativo");
            entity.Property(e => e.Estado)
                .HasDefaultValue(true)
                .HasComment("estado del registro")
                .HasColumnName("estado");
            entity.Property(e => e.Feccre)
                .HasDefaultValueSql("now()")
                .HasComment("fecha de creacion del registro")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("feccre");
            entity.Property(e => e.Fecmod)
                .HasComment("fecha de modificacion del registro")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecmod");
            entity.Property(e => e.IdTanque)
                .HasComment("identificador unico del tanque")
                .HasColumnName("id_tanque");
            entity.Property(e => e.NomResponsable)
                .HasMaxLength(255)
                .HasComment("nombre completo del responsable")
                .HasColumnName("nom_responsable");
            entity.Property(e => e.NombreOperador)
                .HasMaxLength(255)
                .HasComment("nombre del operador")
                .HasColumnName("nombre_operador");
            entity.Property(e => e.NumCert)
                .HasMaxLength(100)
                .HasComment("numero de certificacion de calidad")
                .HasColumnName("num_cert");
            entity.Property(e => e.Observacion)
                .HasComment("nota de observacion")
                .HasColumnName("observacion");
            entity.Property(e => e.Usucre)
                .HasComment("usuario de creacion del registro")
                .HasColumnName("usucre");
            entity.Property(e => e.Usumod)
                .HasComment("usuario que modifico el registro")
                .HasColumnName("usumod");
            entity.Property(e => e.VolRecibido)
                .HasComment("volumen recibido")
                .HasColumnName("vol_recibido");

            entity.HasOne(d => d.IdTanqueNavigation).WithMany(p => p.Almacenados)
                .HasForeignKey(d => d.IdTanque)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_almacenado_tanque");
        });

        modelBuilder.Entity<Despacho>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("despacho_pk");

            entity.ToTable("despacho", "traz_operativo", tb => tb.HasComment("La Distribuidora declara la Factura y Guía de Despacho en unidades de garrafas de 10 kg, consumiendo el saldo del Lote envasado vinculado."));

            entity.HasIndex(e => new { e.Correlativo, e.Operador, e.FechaDespacho, e.Zona, e.PlacaDistribuidor }, "despacho_correlativo_operador_fecha_despacho_zona_placa_distrib");

            entity.HasIndex(e => e.Correlativo, "unq_despacho_correlativo").IsUnique();

            entity.Property(e => e.Id)
                .HasComment("identificador único")
                .HasColumnName("id");
            entity.Property(e => e.CamionDistr)
                .HasMaxLength(100)
                .HasComment("camion distribuidor")
                .HasColumnName("camion_distr");
            entity.Property(e => e.Correlativo)
                .HasComment("numero correlativo")
                .HasColumnName("correlativo");
            entity.Property(e => e.Distribuidor)
                .HasMaxLength(100)
                .HasComment("nombre del distribuidor")
                .HasColumnName("distribuidor");
            entity.Property(e => e.Estado)
                .HasDefaultValue(true)
                .HasComment("estado del requistro")
                .HasColumnName("estado");
            entity.Property(e => e.Feccre)
                .HasDefaultValueSql("now()")
                .HasComment("fecha de creacion del registro ")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("feccre");
            entity.Property(e => e.FechaDespacho)
                .HasDefaultValueSql("now()")
                .HasComment("fecha que se despacho el glp")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_despacho");
            entity.Property(e => e.Fecmod)
                .HasComment("fecha de modificacion del registro")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecmod");
            entity.Property(e => e.NumDespacho)
                .HasComment("numero de garrafas despachadas")
                .HasColumnName("num_despacho");
            entity.Property(e => e.NumFactura)
                .HasMaxLength(100)
                .HasComment("numero de factura")
                .HasColumnName("num_factura");
            entity.Property(e => e.Observacion)
                .HasComment("observacion")
                .HasColumnName("observacion");
            entity.Property(e => e.Operador)
                .HasMaxLength(255)
                .HasComment("nombre del operador ")
                .HasColumnName("operador");
            entity.Property(e => e.PlacaDistribuidor)
                .HasMaxLength(100)
                .HasComment("placa del distribuidor")
                .HasColumnName("placa_distribuidor");
            entity.Property(e => e.UnidadEquivalente)
                .HasMaxLength(100)
                .HasComment("unidad de medida del volumen equivalente ")
                .HasColumnName("unidad_equivalente");
            entity.Property(e => e.Usucre)
                .HasComment("usario de creacion del registro")
                .HasColumnName("usucre");
            entity.Property(e => e.Usumod)
                .HasComment("usuario que modifico el registro")
                .HasColumnName("usumod");
            entity.Property(e => e.VolEquivalente)
                .HasComment("volumen equivalente")
                .HasColumnName("vol_equivalente");
            entity.Property(e => e.Zona)
                .HasComment("zona de distribucion")
                .HasColumnName("zona");
        });

        modelBuilder.Entity<Envasado>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("envasado_pk");

            entity.ToTable("envasado", "traz_operativo");

            entity.HasIndex(e => new { e.Correlativo, e.NumReporte, e.EstadoHermeticidad }, "envasado_correlativo_num_reporte_estado_hermeticidad_index");

            entity.Property(e => e.Id)
                .HasComment("identificador unico")
                .HasColumnName("id");
            entity.Property(e => e.Correlativo)
                .HasComment("correlativo")
                .HasColumnName("correlativo");
            entity.Property(e => e.Estado)
                .HasDefaultValue(true)
                .HasComment("estado del registro 1=activo; 2=inactivo")
                .HasColumnName("estado");
            entity.Property(e => e.EstadoHermeticidad)
                .HasMaxLength(100)
                .HasDefaultValueSql("'Conforme'::character varying")
                .HasComment("estado de hermeticidad")
                .HasColumnName("estado_hermeticidad");
            entity.Property(e => e.Feccre)
                .HasDefaultValueSql("now()")
                .HasComment("fecha de creacion del registro")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("feccre");
            entity.Property(e => e.FechaEnvasado)
                .HasDefaultValueSql("now()")
                .HasComment("fecha del envasado del glp")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_envasado");
            entity.Property(e => e.Fecmod)
                .HasComment("fecha que se modifico el registro")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecmod");
            entity.Property(e => e.NumGarrafas)
                .HasComment("numero de garrafas")
                .HasColumnName("num_garrafas");
            entity.Property(e => e.NumReporte)
                .HasMaxLength(100)
                .HasComment("numero del reporte del envasado")
                .HasColumnName("num_reporte");
            entity.Property(e => e.Observacion)
                .HasComment("observacion")
                .HasColumnName("observacion ");
            entity.Property(e => e.Operador)
                .HasMaxLength(255)
                .HasComment("nombre del operador")
                .HasColumnName("operador");
            entity.Property(e => e.PesoUnitario)
                .HasComment("peso unitario por garrafa")
                .HasColumnName("peso_unitario");
            entity.Property(e => e.SaldoDisponible)
                .HasComment("saldo del volumen disponible")
                .HasColumnName("saldo_disponible");
            entity.Property(e => e.UnidadDespacho)
                .HasMaxLength(100)
                .HasComment("unidad de medida del volumen depachado")
                .HasColumnName("unidad_despacho");
            entity.Property(e => e.UnidadDisponible)
                .HasMaxLength(100)
                .HasComment("unidad de medida del volumen saldo disponible")
                .HasColumnName("unidad_disponible");
            entity.Property(e => e.UnidadEnvasado)
                .HasMaxLength(50)
                .HasComment("unidad de medida del envasado")
                .HasColumnName("unidad_envasado");
            entity.Property(e => e.UnidadPeso)
                .HasMaxLength(50)
                .HasComment("unidad de medida del peso")
                .HasColumnName("unidad_peso");
            entity.Property(e => e.Usucre)
                .HasComment("usuario que creo el registro")
                .HasColumnName("usucre");
            entity.Property(e => e.Usumod)
                .HasComment("usuario que modifico el registro")
                .HasColumnName("usumod");
            entity.Property(e => e.VolDespachado)
                .HasComment("volumen despachado")
                .HasColumnName("vol_despachado");
            entity.Property(e => e.VolEnvasado)
                .HasComment("volumen del envasado")
                .HasColumnName("vol_envasado");
        });

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

        modelBuilder.Entity<Tanque>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("tanque_pk");

            entity.ToTable("tanque", "traz_operativo");

            entity.HasIndex(e => e.NomTanque, "tanque_nom_tanque_index");

            entity.Property(e => e.Id)
                .HasComment("identificador unico")
                .HasColumnName("id");
            entity.Property(e => e.Estado)
                .HasDefaultValue(true)
                .HasComment("estado del registro")
                .HasColumnName("estado");
            entity.Property(e => e.Feccre)
                .HasDefaultValueSql("now()")
                .HasComment("fecha de creacion del registro")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("feccre");
            entity.Property(e => e.Fecmod)
                .HasComment("fecha de modificacion del registro")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecmod");
            entity.Property(e => e.NomTanque)
                .HasMaxLength(30)
                .HasComment("nombre del tanque")
                .HasColumnName("nom_tanque");
            entity.Property(e => e.Observacion)
                .HasComment("observacion")
                .HasColumnName("observacion");
            entity.Property(e => e.SaldoDisp)
                .HasComment("saldo disponible del tanque")
                .HasColumnName("saldo_disp");
            entity.Property(e => e.Usucre)
                .HasComment("usuario de creacion del registro")
                .HasColumnName("usucre");
            entity.Property(e => e.Usumod)
                .HasComment("usuario que modifico el registro")
                .HasColumnName("usumod");
            entity.Property(e => e.VolIngresado)
                .HasComment("volumen ingresado al tanque")
                .HasColumnName("vol_ingresado");
            entity.Property(e => e.VolTotDespachado)
                .HasComment("volumen total despachado")
                .HasColumnName("vol_tot_despachado");
        });

        modelBuilder.Entity<TbCertDetalle>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tb_cert_detalle", "trazabilidad_op");

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
                .HasMaxLength(100)
                .HasColumnName("estado");
            entity.Property(e => e.FechaMuestreo)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_muestreo");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.IdActualizadoPor).HasColumnName("id_actualizado_por");
            entity.Property(e => e.IdCertParam).HasColumnName("id_cert_param");
            entity.Property(e => e.IdCreadoPor).HasColumnName("id_creado_por");
            entity.Property(e => e.IdEliminadoPor).HasColumnName("id_eliminado_por");
            entity.Property(e => e.IdLote).HasColumnName("id_lote");
            entity.Property(e => e.Matadata)
                .HasColumnType("jsonb")
                .HasColumnName("matadata");
            entity.Property(e => e.Observacion).HasColumnName("observacion");
            entity.Property(e => e.PathCert)
                .HasMaxLength(255)
                .HasColumnName("path_cert");
            entity.Property(e => e.Tag)
                .HasMaxLength(255)
                .HasColumnName("tag");
            entity.Property(e => e.TamCert).HasColumnName("tam_cert");
            entity.Property(e => e.UnidadMed)
                .HasMaxLength(50)
                .HasColumnName("unidad_med");
            entity.Property(e => e.VolLote).HasColumnName("vol_lote");

            entity.HasOne(d => d.IdCertParamNavigation).WithMany()
                .HasForeignKey(d => d.IdCertParam)
                .HasConstraintName("fk_tb_cert_detalle_tb_param_cert");
        });

        modelBuilder.Entity<TbCertEspec>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("tb_cert_espec_pk");

            entity.ToTable("tb_cert_espec", "trazabilidad_op");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nextval('trazabilidad_op.cert_espec_id_seq'::regclass)")
                .HasColumnName("id");
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
            entity.Property(e => e.DescEspec).HasColumnName("desc_espec");
            entity.Property(e => e.EliminadoEn)
                .HasColumnType("timestamp(0) without time zone")
                .HasColumnName("eliminado_en");
            entity.Property(e => e.EliminadoPor)
                .HasMaxLength(200)
                .HasColumnName("eliminado_por");
            entity.Property(e => e.Estado)
                .HasMaxLength(100)
                .HasColumnName("estado");
            entity.Property(e => e.IdActualizadoPor).HasColumnName("id_actualizado_por");
            entity.Property(e => e.IdCreadoPor).HasColumnName("id_creado_por");
            entity.Property(e => e.IdEliminadoPor).HasColumnName("id_eliminado_por");
            entity.Property(e => e.Matadata)
                .HasColumnType("jsonb")
                .HasColumnName("matadata");
            entity.Property(e => e.MaxEspec)
                .HasMaxLength(50)
                .HasColumnName("max_espec");
            entity.Property(e => e.MetAltern1)
                .HasMaxLength(10)
                .HasColumnName("met_altern1");
            entity.Property(e => e.MetAltern2)
                .HasMaxLength(10)
                .HasColumnName("met_altern2");
            entity.Property(e => e.MinEspec)
                .HasMaxLength(50)
                .HasColumnName("min_espec");
            entity.Property(e => e.Unidad)
                .HasMaxLength(10)
                .HasColumnName("unidad");
        });

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

            entity.HasOne(d => d.IdCertParamNavigation).WithMany(p => p.TbCertificados)
                .HasForeignKey(d => d.IdCertParam)
                .HasConstraintName("fk_tb_certificado_tb_param_cert");
        });

        modelBuilder.Entity<TbCisterna>(entity =>
        {
            entity.HasKey(e => e.IdCisterna).HasName("tb_cisterna_pkey");

            entity.ToTable("tb_cisterna", tb => tb.HasComment("Unidades de transporte de GLP."));

            entity.Property(e => e.IdCisterna).HasColumnName("id_cisterna");
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
            entity.Property(e => e.IdCisternaDetalle).HasColumnName("id_cisterna_detalle");
            entity.Property(e => e.IdCreadoPor).HasColumnName("id_creado_por");
            entity.Property(e => e.IdEliminadoPor).HasColumnName("id_eliminado_por");
            entity.Property(e => e.IdInstancia).HasColumnName("id_instancia");
            entity.Property(e => e.Matadata)
                .HasColumnType("jsonb")
                .HasColumnName("matadata");
            entity.Property(e => e.Observacion).HasColumnName("observacion");
        });

        modelBuilder.Entity<TbCisternaDetalle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("tb_cisterna_detalle_pk");

            entity.ToTable("tb_cisterna_detalle", "trazabilidad_op");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Activo).HasColumnName("activo");
            entity.Property(e => e.ActualizadoEn)
                .HasColumnType("timestamp(0) without time zone")
                .HasColumnName("actualizado_en");
            entity.Property(e => e.ActualizadoPor)
                .HasMaxLength(200)
                .HasColumnName("actualizado_por");
            entity.Property(e => e.Cliente)
                .HasMaxLength(255)
                .HasColumnName("cliente");
            entity.Property(e => e.Conductor)
                .HasMaxLength(255)
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
            entity.Property(e => e.EmpresaTrans)
                .HasMaxLength(50)
                .HasColumnName("empresa_trans");
            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .HasColumnName("estado");
            entity.Property(e => e.Fecha)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha");
            entity.Property(e => e.Gravedad).HasColumnName("gravedad");
            entity.Property(e => e.IdActualizadoPor).HasColumnName("id_actualizado_por");
            entity.Property(e => e.IdCreadoPor).HasColumnName("id_creado_por");
            entity.Property(e => e.IdEliminadoPor).HasColumnName("id_eliminado_por");
            entity.Property(e => e.Matadata)
                .HasColumnType("jsonb")
                .HasColumnName("matadata");
            entity.Property(e => e.NroCre)
                .HasMaxLength(50)
                .HasColumnName("nro_cre");
            entity.Property(e => e.NroCreFenix)
                .HasMaxLength(50)
                .HasColumnName("nro_cre_fenix");
            entity.Property(e => e.PesoKg).HasColumnName("peso_kg");
            entity.Property(e => e.PesoTm).HasColumnName("peso_tm");
            entity.Property(e => e.Placa)
                .HasMaxLength(50)
                .HasColumnName("placa");
            entity.Property(e => e.PlantaDescarga)
                .HasMaxLength(255)
                .HasColumnName("planta_descarga");
            entity.Property(e => e.VolBbls).HasColumnName("vol_bbls");
            entity.Property(e => e.VolM3).HasColumnName("vol_m3");
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

            entity.ToTable("tb_evento", "trazabilidad_op");

            entity.HasIndex(e => new { e.TipoEvento, e.FechaEvento }, "idx_evento_tipo_fecha");

            entity.Property(e => e.IdEvento).HasColumnName("id_evento");
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
            entity.Property(e => e.Descripcion)
                .HasMaxLength(500)
                .HasColumnName("descripcion");
            entity.Property(e => e.EliminadoEn)
                .HasColumnType("timestamp(0) without time zone")
                .HasColumnName("eliminado_en");
            entity.Property(e => e.EliminadoPor)
                .HasMaxLength(200)
                .HasColumnName("eliminado_por");
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
            entity.Property(e => e.IdActualizadoPor).HasColumnName("id_actualizado_por");
            entity.Property(e => e.IdCreadoPor).HasColumnName("id_creado_por");
            entity.Property(e => e.IdEliminadoPor).HasColumnName("id_eliminado_por");
            entity.Property(e => e.Matadata)
                .HasColumnType("jsonb")
                .HasColumnName("matadata");
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

            entity.ToTable("tb_evento_destino", "trazabilidad_op");

            entity.HasIndex(e => e.IdInstancia, "idx_evento_destino_instancia");

            entity.HasIndex(e => e.IdLote, "idx_evento_destino_lote");

            entity.HasIndex(e => new { e.IdEvento, e.IdInstancia, e.IdLote }, "uq_evento_destino").IsUnique();

            entity.Property(e => e.IdEventoDestino).HasColumnName("id_evento_destino");
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
            entity.Property(e => e.IdActualizadoPor).HasColumnName("id_actualizado_por");
            entity.Property(e => e.IdCreadoPor).HasColumnName("id_creado_por");
            entity.Property(e => e.IdEliminadoPor).HasColumnName("id_eliminado_por");
            entity.Property(e => e.IdEvento).HasColumnName("id_evento");
            entity.Property(e => e.IdInstancia).HasColumnName("id_instancia");
            entity.Property(e => e.IdLote).HasColumnName("id_lote");
            entity.Property(e => e.Matadata)
                .HasColumnType("jsonb")
                .HasColumnName("matadata");
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

            entity.ToTable("tb_evento_origen", "trazabilidad_op");

            entity.HasIndex(e => e.IdInstancia, "idx_evento_origen_instancia");

            entity.HasIndex(e => e.IdLote, "idx_evento_origen_lote");

            entity.HasIndex(e => new { e.IdEvento, e.IdInstancia, e.IdLote }, "uq_evento_origen").IsUnique();

            entity.Property(e => e.IdEventoOrigen).HasColumnName("id_evento_origen");
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
            entity.Property(e => e.IdActualizadoPor).HasColumnName("id_actualizado_por");
            entity.Property(e => e.IdCreadoPor).HasColumnName("id_creado_por");
            entity.Property(e => e.IdEliminadoPor).HasColumnName("id_eliminado_por");
            entity.Property(e => e.IdEvento).HasColumnName("id_evento");
            entity.Property(e => e.IdInstancia).HasColumnName("id_instancia");
            entity.Property(e => e.IdLote).HasColumnName("id_lote");
            entity.Property(e => e.Matadata)
                .HasColumnType("jsonb")
                .HasColumnName("matadata");
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

            entity.ToTable("tb_instancia", "trazabilidad_op");

            entity.HasIndex(e => e.IdTipoLugar, "idx_instancia_tipo");

            entity.HasIndex(e => new { e.IdTipoLugar, e.Codigo }, "uq_instancia_tipo_codigo").IsUnique();

            entity.Property(e => e.IdInstancia).HasColumnName("id_instancia");
            entity.Property(e => e.Activo).HasColumnName("activo");
            entity.Property(e => e.ActualizadoEn)
                .HasColumnType("timestamp(0) without time zone")
                .HasColumnName("actualizado_en");
            entity.Property(e => e.ActualizadoPor)
                .HasMaxLength(200)
                .HasColumnName("actualizado_por");
            entity.Property(e => e.Codigo)
                .HasMaxLength(50)
                .HasColumnName("codigo");
            entity.Property(e => e.CreadoEn)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp(0) without time zone")
                .HasColumnName("creado_en");
            entity.Property(e => e.CreadoPor)
                .HasMaxLength(200)
                .HasColumnName("creado_por");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(250)
                .HasColumnName("descripcion");
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
            entity.Property(e => e.IdTipoLugar).HasColumnName("id_tipo_lugar");
            entity.Property(e => e.Matadata)
                .HasColumnType("jsonb")
                .HasColumnName("matadata");
            entity.Property(e => e.Nombre)
                .HasMaxLength(200)
                .HasColumnName("nombre");
            entity.Property(e => e.Ubicacion)
                .HasMaxLength(250)
                .HasColumnName("ubicacion");

            entity.HasOne(d => d.IdTipoLugarNavigation).WithMany(p => p.TbInstancia)
                .HasForeignKey(d => d.IdTipoLugar)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_instancia_tipo");
        });

        modelBuilder.Entity<TbLoteGlp>(entity =>
        {
            entity.HasKey(e => e.IdLote).HasName("tb_lote_glp_pkey");

            entity.ToTable("tb_lote_glp", "trazabilidad_op");

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

            entity.HasOne(d => d.IdPlantaOrigenNavigation).WithMany(p => p.TbLoteGlps)
                .HasForeignKey(d => d.IdPlantaOrigen)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_lote_planta");
        });

        modelBuilder.Entity<TbParamCert>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("tb_param_cert_pk");

            entity.ToTable("tb_param_cert", "trazabilidad_op");

            entity.HasIndex(e => e.Id, "unq_tb_param_cert_id").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Activo).HasColumnName("activo");
            entity.Property(e => e.ActualizadoEn)
                .HasColumnType("timestamp(0) without time zone")
                .HasColumnName("actualizado_en");
            entity.Property(e => e.ActualizadoPor)
                .HasMaxLength(200)
                .HasColumnName("actualizado_por");
            entity.Property(e => e.AstmUop).HasColumnName("astm_uop");
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
                .HasMaxLength(50)
                .HasColumnName("estado");
            entity.Property(e => e.IdActualizadoPor).HasColumnName("id_actualizado_por");
            entity.Property(e => e.IdCertEspec).HasColumnName("id_cert_espec");
            entity.Property(e => e.IdCreadoPor).HasColumnName("id_creado_por");
            entity.Property(e => e.IdEliminadoPor).HasColumnName("id_eliminado_por");
            entity.Property(e => e.Justificacion).HasColumnName("justificacion");
            entity.Property(e => e.Matadata)
                .HasColumnType("jsonb")
                .HasColumnName("matadata");
            entity.Property(e => e.UniParams)
                .HasMaxLength(10)
                .HasColumnName("uni_params");
            entity.Property(e => e.ValorReportado)
                .HasMaxLength(50)
                .HasColumnName("valor_reportado");

            entity.HasOne(d => d.IdCertEspecNavigation).WithMany(p => p.TbParamCerts)
                .HasForeignKey(d => d.IdCertEspec)
                .HasConstraintName("fk_tb_param_cert_tb_cert_espec");
        });

        modelBuilder.Entity<TbPlantum>(entity =>
        {
            entity.HasKey(e => e.IdPlanta).HasName("tb_planta_pkey");

            entity.ToTable("tb_planta", "trazabilidad_op");

            entity.HasIndex(e => e.IdInstancia, "tb_planta_id_instancia_key").IsUnique();

            entity.Property(e => e.IdPlanta).HasColumnName("id_planta");
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
            entity.Property(e => e.Departamento)
                .HasMaxLength(100)
                .HasColumnName("departamento");
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

            entity.ToTable("tb_tipo_lugar", "trazabilidad_op");

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

        modelBuilder.Entity<TblTrazabilidad1>(entity =>
        {
            entity.HasKey(e => e.CorrDest).HasName("tbl_trazabilidad_pk");

            entity.ToTable("tbl_trazabilidad");

            entity.HasIndex(e => new { e.IdPlanta, e.CorrOri }, "tbl_trazabilidad_id_planta_corr_ori_index");

            entity.Property(e => e.CorrDest)
                .ValueGeneratedNever()
                .HasComment("correlativo destino")
                .HasColumnName("corr_dest");
            entity.Property(e => e.Activo).HasColumnName("activo");
            entity.Property(e => e.ActualizadoEn)
                .HasColumnType("timestamp(0) without time zone")
                .HasColumnName("actualizado_en");
            entity.Property(e => e.ActualizadoPor)
                .HasMaxLength(200)
                .HasColumnName("actualizado_por");
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
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasComment("identificador unico")
                .HasColumnName("id");
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

        modelBuilder.Entity<Transporte>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("transporte_pk");

            entity.ToTable("transporte", "traz_operativo");

            entity.HasIndex(e => new { e.Correlativo, e.Operador }, "transporte_correlativo_operador_index");

            entity.HasIndex(e => e.Correlativo, "unq_transporte_correlativo").IsUnique();

            entity.Property(e => e.Id)
                .HasComment("identificador unico de la tabla transporte")
                .HasColumnName("id");
            entity.Property(e => e.Activo).HasColumnName("activo");
            entity.Property(e => e.ActualizadoEn)
                .HasColumnType("timestamp(0) without time zone")
                .HasColumnName("actualizado_en");
            entity.Property(e => e.ActualizadoPor)
                .HasMaxLength(200)
                .HasColumnName("actualizado_por");
            entity.Property(e => e.Conductor)
                .HasComment("nombre del conductor ")
                .HasColumnName("conductor");
            entity.Property(e => e.Correlativo)
                .HasComment("numero correlativo ")
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
            entity.Property(e => e.FechaHoraCarga)
                .HasDefaultValueSql("now()")
                .HasComment("fecha y hora de carga del glp ")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_hora_carga");
            entity.Property(e => e.IdActualizadoPor).HasColumnName("id_actualizado_por");
            entity.Property(e => e.IdCreadoPor).HasColumnName("id_creado_por");
            entity.Property(e => e.IdEliminadoPor).HasColumnName("id_eliminado_por");
            entity.Property(e => e.Metadata)
                .HasColumnType("jsonb")
                .HasColumnName("metadata");
            entity.Property(e => e.NumCert)
                .HasComment("numero de certificado de calidad")
                .HasColumnType("character varying")
                .HasColumnName("num_cert");
            entity.Property(e => e.NumPrecinto)
                .HasComment("numero del precinto")
                .HasColumnType("character varying")
                .HasColumnName("num_precinto");
            entity.Property(e => e.Observacion)
                .HasComment("observacion")
                .HasColumnName("observacion");
            entity.Property(e => e.Operador)
                .HasComment("nombre del operador")
                .HasColumnName("operador");
            entity.Property(e => e.Placa)
                .HasMaxLength(10)
                .HasComment("numero de placa del transporte")
                .HasColumnName("placa");
            entity.Property(e => e.VolTransportado)
                .HasComment("volumen transportado ")
                .HasColumnName("vol_transportado");
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
        modelBuilder.HasSequence("seq_idproduccion", "traz_operativo");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
