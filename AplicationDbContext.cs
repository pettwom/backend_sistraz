using Microsoft.EntityFrameworkCore;
using backend_trazabilidad.Models;

namespace backend_trazabilidad
{
    public class AplicationDbContext:DbContext
    {

        public AplicationDbContext(DbContextOptions<AplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Produccion> Produccion { get; set; }

    }
}
 