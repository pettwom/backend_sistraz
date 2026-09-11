using Microsoft.EntityFrameworkCore;

namespace backend_trazabilidad
{
    public class PostgresDbContext:DbContext
    {
        public PostgresDbContext(DbContextOptions<PostgresDbContext>options):base (options) { }
    }
}
