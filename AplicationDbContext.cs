using Microsoft.EntityFrameworkCore;
using backend_trazabilidad.Models;
using OracleScanffold.Models.Oracle;

namespace backend_trazabilidad
{
    public class AplicationDbContext:DbContext
    {

        public AplicationDbContext(DbContextOptions<AplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Produccion> Produccion { get; set; }

        public DbSet<TsegUsuario> Usuario { get; set; }
        public DbSet<TsegPerfilesUsuario> PerfilUsuario { get; set; }
        public DbSet<TsegPerfile> Perfiles { get; set; }
        public DbSet<TsegMenuesPerfil> MenuesPerfil { get; set; }
        public DbSet<TsegMenue> Menues { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            entity.HasOne(d => d.Plantum).WithOne(p => p.IdNavigation)
            .HasForeignKey<Produccion>(d => d.IdPlanta)
            .HasConstraintName("produccion_id_planta_fkey");
        }
    }
}
 