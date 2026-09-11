using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace backend_trazabilidad.Models;

public partial class TrazabilidadDbContext : DbContext
{
    public TrazabilidadDbContext()
    {
    }

    public TrazabilidadDbContext(DbContextOptions<TrazabilidadDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Almacenado> Almacenados { get; set; }

    public virtual DbSet<Despacho> Despachos { get; set; }

    public virtual DbSet<Envasado> Envasados { get; set; }

    public virtual DbSet<Plantum> Planta { get; set; }

    public virtual DbSet<Produccion> Produccions { get; set; }

    public virtual DbSet<Tanque> Tanques { get; set; }

    public virtual DbSet<TblTrazabilidad> TblTrazabilidads { get; set; }

    public virtual DbSet<Transporte> Transportes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Host=localhost;Database=trazabilidad_db;Username=postgres;Password=qwerty");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<TblTrazabilidad>()
            .HasOne(t => t.CorrDest1)
            .WithMany(d => d.TblTrazabilidades)
            .HasForeignKey(t => t.CorrDest)
            .HasPrincipalKey(d => d.Id);

        modelBuilder.Entity<TblTrazabilidad>()
            .Ignore(t => t.CorrDest2);

        modelBuilder.Entity<TblTrazabilidad>()
            .Ignore(t => t.CorrDestNavigation);
        //modelBuilder.Entity<Almacenado>(entity =>
        //{
        //    entity.HasKey(e => e.Id).HasName("almcenado_pk");

        //    entity.ToTable("almacenado", "traz_operativo");

        //    entity.HasIndex(e => new { e.Correlativo, e.NombreOperador, e.NumCert }, "almcenado_correlativo_nombre_operador_num_cert_index");

        //    entity.HasIndex(e => e.Correlativo, "unq_almacenado_correlativo").IsUnique();

        //    entity.Property(e => e.Id)
        //        .HasDefaultValueSql("nextval('traz_operativo.almcenado_id_seq'::regclass)")
        //        .HasComment("identificador unico")
        //        .HasColumnName("id");
        //    entity.Property(e => e.Correlativo)
        //        .IsRequired()
        //        .HasComment("correlativo")
        //        .HasColumnName("correlativo");
        //    entity.Property(e => e.Estado)
        //        .HasDefaultValue(true)
        //        .HasComment("estado del registro")
        //        .HasColumnName("estado");
        //    entity.Property(e => e.Feccre)
        //        .HasDefaultValueSql("now()")
        //        .HasComment("fecha de creacion del registro")
        //        .HasColumnType("timestamp without time zone")
        //        .HasColumnName("feccre");
        //    entity.Property(e => e.Fecmod)
        //        .HasComment("fecha de modificacion del registro")
        //        .HasColumnType("timestamp without time zone")
        //        .HasColumnName("fecmod");
        //    entity.Property(e => e.IdTanque)
        //        .HasComment("identificador unico del tanque")
        //        .HasColumnName("id_tanque");
        //    entity.Property(e => e.NomResponsable)
        //        .HasMaxLength(255)
        //        .HasComment("nombre completo del responsable")
        //        .HasColumnName("nom_responsable");
        //    entity.Property(e => e.NombreOperador)
        //        .HasMaxLength(255)
        //        .HasComment("nombre del operador")
        //        .HasColumnName("nombre_operador");
        //    entity.Property(e => e.NumCert)
        //        .HasMaxLength(100)
        //        .HasComment("numero de certificacion de calidad")
        //        .HasColumnName("num_cert");
        //    entity.Property(e => e.Observacion)
        //        .HasComment("nota de observacion")
        //        .HasColumnName("observacion");
        //    entity.Property(e => e.Usucre)
        //        .HasComment("usuario de creacion del registro")
        //        .HasColumnName("usucre");
        //    entity.Property(e => e.Usumod)
        //        .HasComment("usuario que modifico el registro")
        //        .HasColumnName("usumod");
        //    entity.Property(e => e.VolRecibido)
        //        .HasComment("volumen recibido")
        //        .HasColumnName("vol_recibido");

        //    entity.HasOne(d => d.IdTanqueNavigation).WithMany(p => p.Almacenados)
        //        .HasForeignKey(d => d.IdTanque)
        //        .OnDelete(DeleteBehavior.ClientSetNull)
        //        .HasConstraintName("fk_almacenado_tanque");
        //});

        //modelBuilder.Entity<Despacho>(entity =>
        //{
        //    entity.HasKey(e => e.Id).HasName("despacho_pk");

        //    entity.ToTable("despacho", "traz_operativo", tb => tb.HasComment("La Distribuidora declara la Factura y Guía de Despacho en unidades de garrafas de 10 kg, consumiendo el saldo del Lote envasado vinculado."));

        //    entity.HasIndex(e => new { e.Correlativo, e.Operador, e.FechaDespacho, e.Zona, e.PlacaDistribuidor }, "despacho_correlativo_operador_fecha_despacho_zona_placa_distrib");

        //    entity.HasIndex(e => e.Correlativo, "unq_despacho_correlativo").IsUnique();

        //    entity.Property(e => e.Id)
        //        .HasComment("identificador único")
        //        .HasColumnName("id");
        //    entity.Property(e => e.CamionDistr)
        //        .HasMaxLength(100)
        //        .HasComment("camion distribuidor")
        //        .HasColumnName("camion_distr");
        //    entity.Property(e => e.Correlativo)
        //        .HasComment("numero correlativo")
        //        .HasColumnName("correlativo");
        //    entity.Property(e => e.Distribuidor)
        //        .HasMaxLength(100)
        //        .HasComment("nombre del distribuidor")
        //        .HasColumnName("distribuidor");
        //    entity.Property(e => e.Estado)
        //        .HasDefaultValue(true)
        //        .HasComment("estado del requistro")
        //        .HasColumnName("estado");
        //    entity.Property(e => e.Feccre)
        //        .HasDefaultValueSql("now()")
        //        .HasComment("fecha de creacion del registro ")
        //        .HasColumnType("timestamp without time zone")
        //        .HasColumnName("feccre");
        //    entity.Property(e => e.FechaDespacho)
        //        .HasDefaultValueSql("now()")
        //        .HasComment("fecha que se despacho el glp")
        //        .HasColumnType("timestamp without time zone")
        //        .HasColumnName("fecha_despacho");
        //    entity.Property(e => e.Fecmod)
        //        .HasComment("fecha de modificacion del registro")
        //        .HasColumnType("timestamp without time zone")
        //        .HasColumnName("fecmod");
        //    entity.Property(e => e.NumDespacho)
        //        .HasComment("numero de garrafas despachadas")
        //        .HasColumnName("num_despacho");
        //    entity.Property(e => e.NumFactura)
        //        .HasMaxLength(100)
        //        .HasComment("numero de factura")
        //        .HasColumnName("num_factura");
        //    entity.Property(e => e.Observacion)
        //        .HasComment("observacion")
        //        .HasColumnName("observacion");
        //    entity.Property(e => e.Operador)
        //        .HasMaxLength(255)
        //        .HasComment("nombre del operador ")
        //        .HasColumnName("operador");
        //    entity.Property(e => e.PlacaDistribuidor)
        //        .HasMaxLength(100)
        //        .HasComment("placa del distribuidor")
        //        .HasColumnName("placa_distribuidor");
        //    entity.Property(e => e.UnidadEquivalente)
        //        .HasMaxLength(100)
        //        .HasComment("unidad de medida del volumen equivalente ")
        //        .HasColumnName("unidad_equivalente");
        //    entity.Property(e => e.Usucre)
        //        .HasComment("usario de creacion del registro")
        //        .HasColumnName("usucre");
        //    entity.Property(e => e.Usumod)
        //        .HasComment("usuario que modifico el registro")
        //        .HasColumnName("usumod");
        //    entity.Property(e => e.VolEquivalente)
        //        .HasComment("volumen equivalente")
        //        .HasColumnName("vol_equivalente");
        //    entity.Property(e => e.Zona)
        //        .HasComment("zona de distribucion")
        //        .HasColumnName("zona");
        //});

        //modelBuilder.Entity<Envasado>(entity =>
        //{
        //    entity.HasKey(e => e.Id).HasName("envasado_pk");

        //    entity.ToTable("envasado", "traz_operativo");

        //    entity.HasIndex(e => new { e.Correlativo, e.NumReporte, e.EstadoHermeticidad }, "envasado_correlativo_num_reporte_estado_hermeticidad_index");

        //    entity.Property(e => e.Id)
        //        .HasComment("identificador unico")
        //        .HasColumnName("id");
        //    entity.Property(e => e.Correlativo)
        //        .HasComment("correlativo")
        //        .HasColumnName("correlativo");
        //    entity.Property(e => e.Estado)
        //        .HasDefaultValue(true)
        //        .HasComment("estado del registro 1=activo; 2=inactivo")
        //        .HasColumnName("estado");
        //    entity.Property(e => e.EstadoHermeticidad)
        //        .HasMaxLength(100)
        //        .HasDefaultValueSql("'Conforme'::character varying")
        //        .HasComment("estado de hermeticidad")
        //        .HasColumnName("estado_hermeticidad");
        //    entity.Property(e => e.Feccre)
        //        .HasDefaultValueSql("now()")
        //        .HasComment("fecha de creacion del registro")
        //        .HasColumnType("timestamp without time zone")
        //        .HasColumnName("feccre");
        //    entity.Property(e => e.FechaEnvasado)
        //        .HasDefaultValueSql("now()")
        //        .HasComment("fecha del envasado del glp")
        //        .HasColumnType("timestamp without time zone")
        //        .HasColumnName("fecha_envasado");
        //    entity.Property(e => e.Fecmod)
        //        .HasComment("fecha que se modifico el registro")
        //        .HasColumnType("timestamp without time zone")
        //        .HasColumnName("fecmod");
        //    entity.Property(e => e.NumGarrafas)
        //        .HasComment("numero de garrafas")
        //        .HasColumnName("num_garrafas");
        //    entity.Property(e => e.NumReporte)
        //        .HasMaxLength(100)
        //        .HasComment("numero del reporte del envasado")
        //        .HasColumnName("num_reporte");
        //    entity.Property(e => e.Observacion)
        //        .HasComment("observacion")
        //        .HasColumnName("observacion ");
        //    entity.Property(e => e.Operador)
        //        .HasMaxLength(255)
        //        .HasComment("nombre del operador")
        //        .HasColumnName("operador");
        //    entity.Property(e => e.PesoUnitario)
        //        .HasComment("peso unitario por garrafa")
        //        .HasColumnName("peso_unitario");
        //    entity.Property(e => e.SaldoDisponible)
        //        .HasComment("saldo del volumen disponible")
        //        .HasColumnName("saldo_disponible");
        //    entity.Property(e => e.UnidadDespacho)
        //        .HasMaxLength(100)
        //        .HasComment("unidad de medida del volumen depachado")
        //        .HasColumnName("unidad_despacho");
        //    entity.Property(e => e.UnidadDisponible)
        //        .HasMaxLength(100)
        //        .HasComment("unidad de medida del volumen saldo disponible")
        //        .HasColumnName("unidad_disponible");
        //    entity.Property(e => e.UnidadEnvasado)
        //        .HasMaxLength(50)
        //        .HasComment("unidad de medida del envasado")
        //        .HasColumnName("unidad_envasado");
        //    entity.Property(e => e.UnidadPeso)
        //        .HasMaxLength(50)
        //        .HasComment("unidad de medida del peso")
        //        .HasColumnName("unidad_peso");
        //    entity.Property(e => e.Usucre)
        //        .HasComment("usuario que creo el registro")
        //        .HasColumnName("usucre");
        //    entity.Property(e => e.Usumod)
        //        .HasComment("usuario que modifico el registro")
        //        .HasColumnName("usumod");
        //    entity.Property(e => e.VolDespachado)
        //        .HasComment("volumen despachado")
        //        .HasColumnName("vol_despachado");
        //    entity.Property(e => e.VolEnvasado)
        //        .HasComment("volumen del envasado")
        //        .HasColumnName("vol_envasado");

        //    entity.HasOne(d => d.CorrelativoNavigation).WithMany(p => p.Envasados)
        //        .HasForeignKey(d => d.Correlativo)
        //        .OnDelete(DeleteBehavior.ClientSetNull)
        //        .HasConstraintName("fk_envasado_tbl_trazabilidad");
        //});

        //modelBuilder.Entity<Plantum>(entity =>
        //{
        //    entity.HasKey(e => e.Id).HasName("planta_pk");

        //    entity.ToTable("planta", "traz_operativo");

        //    entity.Property(e => e.Id)
        //        .ValueGeneratedOnAdd()
        //        .HasComment("identificador unico")
        //        .HasColumnName("id");
        //    entity.Property(e => e.Nombre)
        //        .HasMaxLength(255)
        //        .HasComment("nombre de la planta o del operador")
        //        .HasColumnName("nombre");
        //    entity.Property(e => e.Pais)
        //        .HasMaxLength(100)
        //        .HasComment("pais de importacion")
        //        .HasColumnName("pais");
        //    entity.Property(e => e.PuntoIngreso)
        //        .HasMaxLength(100)
        //        .HasComment("punto de ingreso al pais")
        //        .HasColumnName("punto_ingreso");
        //    entity.Property(e => e.TipoOp)
        //        .HasDefaultValue(1)
        //        .HasComment("tipo de operacion 1=planta local; 2=importacion")
        //        .HasColumnName("tipo_op");
        //    entity.Property(e => e.VolTotal)
        //        .HasComment("volumen total expresado en Toneladas")
        //        .HasColumnName("vol_total");

        //    //entity.HasOne(d => d.IdNavigation).WithOne(p => p.Plantum)
        //    //    .HasPrincipalKey<Produccion>(p => p.IdPlanta)
        //    //    .HasForeignKey<Plantum>(d => d.Id)
        //    //    .OnDelete(DeleteBehavior.ClientSetNull)
        //    //    .HasConstraintName("fk_planta_produccion");
        //});

        //modelBuilder.Entity<Produccion>(entity =>
        //{
        //    entity.HasKey(e => e.Id).HasName("produccion_pk");

        //    entity.ToTable("produccion", "traz_operativo");

        //    entity.HasIndex(e => e.NumCertCal, "produccion_num_cert_cal_index");

        //    entity.HasIndex(e => e.IdPlanta, "unq_produccion_id_planta").IsUnique();

        //    entity.Property(e => e.Id)
        //        .HasComment("identificador unico para produccion")
        //        .HasColumnName("id");
        //    entity.Property(e => e.Correlativo)
        //        .HasComment("numero correlativo enlazado a tbl_trazabilidad")
        //        .HasColumnName("correlativo");
        //    entity.Property(e => e.Estado)
        //        .HasComment("estado del registro 1= activo; 2= inactivo")
        //        .HasColumnType("timestamp without time zone")
        //        .HasColumnName("estado");
        //    entity.Property(e => e.Feccre)
        //        .HasDefaultValueSql("now()")
        //        .HasComment("fecha de creacion del registro")
        //        .HasColumnType("timestamp without time zone")
        //        .HasColumnName("feccre");
        //    entity.Property(e => e.Fecmod)
        //        .HasComment("fecha de modificacion del registro")
        //        .HasColumnType("timestamp without time zone")
        //        .HasColumnName("fecmod");
        //    entity.Property(e => e.IdPlanta)
        //        .HasComment("Identificador de la planta o operador")
        //        .HasColumnName("id_planta");
        //    entity.Property(e => e.NumCertCal)
        //        .HasComment("numero de certificado de calidad")
        //        .HasColumnType("character varying")
        //        .HasColumnName("num_cert_cal");
        //    entity.Property(e => e.Observacion)
        //        .HasComment("observacion de la produccion")
        //        .HasColumnName("observacion");
        //    entity.Property(e => e.Usucre)
        //        .HasDefaultValue(1)
        //        .HasComment("usuario que creo el registro")
        //        .HasColumnName("usucre");
        //    entity.Property(e => e.Usumod)
        //        .HasComment("usuario de modificacion")
        //        .HasColumnName("usumod");
        //    entity.HasOne(d => d.Plantum).WithOne(p => p.IdNavigation)
        //        .HasForeignKey<Produccion>(d => d.IdPlanta)
        //        .HasConstraintName("produccion_id_planta_fkey");
        //    entity.HasOne(d => d.CorrelativoNavigation).WithMany(p => p.Produccions)
        //        .HasForeignKey(d => d.Correlativo)
        //        .HasConstraintName("fk_produccion_tbl_trazabilidad_0");
        //});

        //modelBuilder.Entity<Tanque>(entity =>
        //{
        //    entity.HasKey(e => e.Id).HasName("tanque_pk");

        //    entity.ToTable("tanque", "traz_operativo");

        //    entity.HasIndex(e => e.NomTanque, "tanque_nom_tanque_index");

        //    entity.Property(e => e.Id)
        //        .HasComment("identificador unico")
        //        .HasColumnName("id");
        //    entity.Property(e => e.Estado)
        //        .HasDefaultValue(true)
        //        .HasComment("estado del registro")
        //        .HasColumnName("estado");
        //    entity.Property(e => e.Feccre)
        //        .HasDefaultValueSql("now()")
        //        .HasComment("fecha de creacion del registro")
        //        .HasColumnType("timestamp without time zone")
        //        .HasColumnName("feccre");
        //    entity.Property(e => e.Fecmod)
        //        .HasComment("fecha de modificacion del registro")
        //        .HasColumnType("timestamp without time zone")
        //        .HasColumnName("fecmod");
        //    entity.Property(e => e.NomTanque)
        //        .HasMaxLength(30)
        //        .HasComment("nombre del tanque")
        //        .HasColumnName("nom_tanque");
        //    entity.Property(e => e.Observacion)
        //        .HasComment("observacion")
        //        .HasColumnName("observacion");
        //    entity.Property(e => e.SaldoDisp)
        //        .HasComment("saldo disponible del tanque")
        //        .HasColumnName("saldo_disp");
        //    entity.Property(e => e.Usucre)
        //        .HasComment("usuario de creacion del registro")
        //        .HasColumnName("usucre");
        //    entity.Property(e => e.Usumod)
        //        .HasComment("usuario que modifico el registro")
        //        .HasColumnName("usumod");
        //    entity.Property(e => e.VolIngresado)
        //        .HasComment("volumen ingresado al tanque")
        //        .HasColumnName("vol_ingresado");
        //    entity.Property(e => e.VolTotDespachado)
        //        .HasComment("volumen total despachado")
        //        .HasColumnName("vol_tot_despachado");
        //});

        //modelBuilder.Entity<TblTrazabilidad>(entity =>
        //{
        //    entity.HasKey(e => e.CorrDest).HasName("tbl_trazabilidad_pk");

        //    entity.ToTable("tbl_trazabilidad", "traz_operativo");

        //    entity.HasIndex(e => new { e.IdPlanta, e.CorrOri }, "tbl_trazabilidad_id_planta_corr_ori_index");

        //    entity.Property(e => e.CorrDest)
        //        .ValueGeneratedNever()
        //        .HasComment("correlativo destino")
        //        .HasColumnName("corr_dest");
        //    entity.Property(e => e.CorrOri)
        //        .HasComment("correlativo de origen")
        //        .HasColumnName("corr_ori");
        //    entity.Property(e => e.Estado)
        //        .HasDefaultValue(true)
        //        .HasComment("estado del registro")
        //        .HasColumnName("estado");
        //    entity.Property(e => e.Feccre)
        //        .HasDefaultValueSql("now()")
        //        .HasComment("fecha de creacion del registro")
        //        .HasColumnType("timestamp without time zone")
        //        .HasColumnName("feccre");
        //    entity.Property(e => e.Fecmod)
        //        .HasComment("fecha que se modifico el registro")
        //        .HasColumnType("timestamp without time zone")
        //        .HasColumnName("fecmod");
        //    entity.Property(e => e.Id)
        //        .ValueGeneratedOnAdd()
        //        .HasComment("identificador unico")
        //        .HasColumnName("id");
        //    entity.Property(e => e.IdPlanta).HasColumnName("id_planta");
        //    entity.Property(e => e.Lugar)
        //        .HasComment("lugar del flujo")
        //        .HasColumnType("character varying")
        //        .HasColumnName("lugar");
        //    entity.Property(e => e.MesAnio)
        //        .HasDefaultValueSql("(date_trunc('month'::text, (CURRENT_DATE)::timestamp with time zone))::date")
        //        .HasColumnType("character varying")
        //        .HasColumnName("mes-anio");
        //    entity.Property(e => e.Usucre)
        //        .HasDefaultValue(1)
        //        .HasComment("usuario de creacion del registro")
        //        .HasColumnName("usucre");
        //    entity.Property(e => e.Usumod)
        //        .HasDefaultValue(0)
        //        .HasComment("usuario que modifico el registro")
        //        .HasColumnName("usumod");

        //    entity.HasOne(d => d.CorrDestNavigation).WithOne(p => p.TblTrazabilidad)
        //        .HasPrincipalKey<Almacenado>(p => p.Correlativo)
        //        .HasForeignKey<TblTrazabilidad>(d => d.CorrDest)
        //        .OnDelete(DeleteBehavior.ClientSetNull)
        //        .HasConstraintName("fk_tbl_trazabilidad_almacenado");

        //    entity.HasOne(d => d.CorrDest1).WithOne(p => p.TblTrazabilidad)
        //        .HasPrincipalKey<Despacho>(p => p.Correlativo)
        //        .HasForeignKey<TblTrazabilidad>(d => d.CorrDest)
        //        .OnDelete(DeleteBehavior.ClientSetNull)
        //        .HasConstraintName("fk_tbl_trazabilidad_despacho");

        //    entity.HasOne(d => d.CorrDest2).WithOne(p => p.TblTrazabilidad)
        //        .HasPrincipalKey<Transporte>(p => p.Correlativo)
        //        .HasForeignKey<TblTrazabilidad>(d => d.CorrDest)
        //        .OnDelete(DeleteBehavior.ClientSetNull)
        //        .HasConstraintName("fk_tbl_trazabilidad_transporte");
        //});

        //modelBuilder.Entity<Transporte>(entity =>
        //{
        //    entity.HasKey(e => e.Id).HasName("transporte_pk");

        //    entity.ToTable("transporte", "traz_operativo");

        //    entity.HasIndex(e => new { e.Correlativo, e.Operador }, "transporte_correlativo_operador_index");

        //    entity.HasIndex(e => e.Correlativo, "unq_transporte_correlativo").IsUnique();

        //    entity.Property(e => e.Id)
        //        .HasComment("identificador unico de la tabla transporte")
        //        .HasColumnName("id");
        //    entity.Property(e => e.Conductor)
        //        .HasComment("nombre del conductor ")
        //        .HasColumnName("conductor");
        //    entity.Property(e => e.Correlativo)
        //        .HasComment("numero correlativo ")
        //        .HasColumnName("correlativo");
        //    entity.Property(e => e.Estado)
        //        .HasDefaultValue(true)
        //        .HasComment("estado del registro")
        //        .HasColumnName("estado");
        //    entity.Property(e => e.Feccre)
        //        .HasDefaultValueSql("now()")
        //        .HasComment("fecha de creacion del registro")
        //        .HasColumnType("timestamp without time zone")
        //        .HasColumnName("feccre");
        //    entity.Property(e => e.FechaHoraCarga)
        //        .HasDefaultValueSql("now()")
        //        .HasComment("fecha y hora de carga del glp ")
        //        .HasColumnType("timestamp without time zone")
        //        .HasColumnName("fecha_hora_carga");
        //    entity.Property(e => e.Fecmod)
        //        .HasComment("fecha de modificacion del regitro")
        //        .HasColumnType("timestamp without time zone")
        //        .HasColumnName("fecmod");
        //    entity.Property(e => e.NumCert)
        //        .HasComment("numero de certificado de calidad")
        //        .HasColumnType("character varying")
        //        .HasColumnName("num_cert");
        //    entity.Property(e => e.NumPrecinto)
        //        .HasComment("numero del precinto")
        //        .HasColumnType("character varying")
        //        .HasColumnName("num_precinto");
        //    entity.Property(e => e.Observacion)
        //        .HasComment("observacion")
        //        .HasColumnName("observacion");
        //    entity.Property(e => e.Operador)
        //        .HasComment("nombre del operador")
        //        .HasColumnName("operador");
        //    entity.Property(e => e.Placa)
        //        .HasMaxLength(6)
        //        .HasComment("numero de placa del transporte")
        //        .HasColumnName("placa");
        //    entity.Property(e => e.Usucre)
        //        .HasComment("usuario de creacion del registro ")
        //        .HasColumnName("usucre");
        //    entity.Property(e => e.Usumod)
        //        .HasComment("usuario que modifico el registro")
        //        .HasColumnName("usumod");
        //    entity.Property(e => e.VolTotal)
        //        .HasComment("volumen total de la planta o operador")
        //        .HasColumnName("vol_total");
        //    entity.Property(e => e.VolTransportado)
        //        .HasComment("volumen transportado ")
        //        .HasColumnName("vol_transportado");
        //});
        //modelBuilder.HasSequence("seq_idproduccion", "traz_operativo");

        //OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
