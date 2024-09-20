using Microsoft.EntityFrameworkCore;
using OtelRezervasyon.Domain;

namespace OtelRezervasyon.Persistence
{
    public class OtelRezervasyonDbContext : DbContext
    {
        public OtelRezervasyonDbContext(DbContextOptions<OtelRezervasyonDbContext> options)
            : base(options)
        {
        }

        public DbSet<Otel> Oteller { get; set; } // Örnek bir DbSet

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Özel konfigürasyonlar burada yapılabilir.
        }
    }
}
