using Microsoft.EntityFrameworkCore;
using OracleScanffold.Models.Oracle;

namespace backend_trazabilidad
{
    public class AplicationDbContext : DbContext
    {
        public AplicationDbContext(
            DbContextOptions<AplicationDbContext> options
        ) : base(options)
        {
        }

        // =========================================================
        // TABLAS ORACLE - SEGURIDAD ANH
        // =========================================================

        public DbSet<TsegUsuario> Usuario { get; set; }

        public DbSet<TsegPerfilesUsuario> PerfilUsuario { get; set; }

        public DbSet<TsegPerfile> Perfiles { get; set; }

        public DbSet<TsegMenuesPerfil> MenuesPerfil { get; set; }

        public DbSet<TsegMenue> Menues { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            // =====================================================
            // ANH_SEG.TSEG_USUARIOS
            // =====================================================

            modelBuilder.Entity<TsegUsuario>(entity =>
            {
                entity.ToTable(
                    "TSEG_USUARIOS",
                    "ANH_SEG"
                );

                entity.HasKey(e => e.IdUsuario);

                entity.Property(e => e.IdUsuario)
                    .HasColumnName("ID_USUARIO");

                entity.Property(e => e.Login)
                    .HasColumnName("LOGIN");

                entity.Property(e => e.Clave)
                    .HasColumnName("CLAVE");

                entity.Property(e => e.Estado)
                    .HasColumnName("ESTADO");

                entity.Property(e => e.AudEstado)
                    .HasColumnName("AUD_ESTADO")
                    .HasColumnType("NUMBER(1)")
                    .HasDefaultValue((short)0);

                entity.Property(e => e.AudUsuario)
                    .HasColumnName("AUD_USUARIO");

                entity.Property(e => e.AudFecha)
                    .HasColumnName("AUD_FECHA");

                entity.Property(e => e.ClaveSalt)
                    .HasColumnName("CLAVE_SALT");

                entity.Property(e => e.AppIdUsuario)
                    .HasColumnName("APP_ID_USUARIO");

                entity.Property(e => e.VigenciaDesde)
                    .HasColumnName("VIGENCIA_DESDE");

                entity.Property(e => e.VigenciaHasta)
                    .HasColumnName("VIGENCIA_HASTA");
            });


            // =====================================================
            // ANH_SEG.TSEG_PERFILES
            // =====================================================

            modelBuilder.Entity<TsegPerfile>(entity =>
            {
                entity.ToTable(
                    "TSEG_PERFILES",
                    "ANH_SEG"
                );

                entity.HasKey(e => e.IdPerfil);

                entity.Property(e => e.IdPerfil)
                    .HasColumnName("ID_PERFIL");

                entity.Property(e => e.NombrePerfil)
                    .HasColumnName("NOMBRE_PERFIL");
            });


            // =====================================================
            // ANH_SEG.TSEG_PERFILES_USUARIO
            // =====================================================

            modelBuilder.Entity<TsegPerfilesUsuario>(entity =>
            {
                entity.ToTable(
                    "TSEG_PERFILES_USUARIO",
                    "ANH_SEG"
                );

                /*
                 * Si tu tabla tiene una PK propia, por ejemplo:
                 *
                 * ID_PERFIL_USUARIO
                 *
                 * debemos usar esa propiedad.
                 *
                 * Mientras no exista una PK propia en la clase,
                 * podemos usar la combinación:
                 *
                 * ID_USUARIO + ID_PERFIL
                 */

                entity.HasKey(e => new
                {
                    e.IdUsuario,
                    e.IdPerfil
                });

                entity.Property(e => e.IdUsuario)
                    .HasColumnName("ID_USUARIO");

                entity.Property(e => e.IdPerfil)
                    .HasColumnName("ID_PERFIL");
            });


            // =====================================================
            // ANH_SEG.TSEG_MENUES_PERFIL
            // =====================================================

            modelBuilder.Entity<TsegMenuesPerfil>(entity =>
            {
                entity.ToTable(
                    "TSEG_MENUES_PERFIL",
                    "ANH_SEG"
                );

                /*
                 * Si IdMenuPerfil realmente existe en tu clase
                 * y en Oracle como ID_MENU_PERFIL:
                 */

                entity.HasKey(e => e.IdMenuPerfil);

                entity.Property(e => e.IdMenuPerfil)
                    .HasColumnName("ID_MENU_PERFIL");

                entity.Property(e => e.IdPerfil)
                    .HasColumnName("ID_PERFIL");

                entity.Property(e => e.IdMenu)
                    .HasColumnName("ID_MENU");
            });


            // =====================================================
            // ANH_SEG.TSEG_MENUES
            // =====================================================

            modelBuilder.Entity<TsegMenue>(entity =>
            {
                entity.ToTable(
                    "TSEG_MENUES",
                    "ANH_SEG"
                );

                entity.HasKey(e => e.IdMenu);

                entity.Property(e => e.IdMenu)
                    .HasColumnName("ID_MENU");

                entity.Property(e => e.Titulo)
                    .HasColumnName("TITULO");

                entity.Property(e => e.Enlace)
                    .HasColumnName("ENLACE");

                entity.Property(e => e.IdMenuPadre)
                    .HasColumnName("ID_MENU_PADRE");

                entity.Property(e => e.Icono)
                    .HasColumnName("ICONO");

                entity.Property(e => e.Orden)
                    .HasColumnName("ORDEN");

                entity.Property(e => e.Descripcion)
                    .HasColumnName("DESCRIPCION");

                entity.Property(e => e.IdModulo)
                    .HasColumnName("ID_MODULO");
            });
        }
    }
}