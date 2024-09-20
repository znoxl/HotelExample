using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace OtelRezervasyon.Persistence
{
    public class OtelRezervasyonDbContextFactory : IDesignTimeDbContextFactory<OtelRezervasyonDbContext>
    {
        public OtelRezervasyonDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<OtelRezervasyonDbContext>();
            optionsBuilder.UseSqlServer("Server=DESKTOP-RE759FF\\SQLEXPRESS;Database=otel-rezervasyon;Trusted_Connection=True;TrustServerCertificate=True");

            return new OtelRezervasyonDbContext(optionsBuilder.Options);
        }
    }
}
