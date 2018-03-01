using Buzy.DataAccess.Model;
using Microsoft.EntityFrameworkCore;

namespace Buzy.DataAccess
{
    public class BusHelperContext : DbContext
    {
        public BusHelperContext(DbContextOptions<BusHelperContext> options) : base(options)
        {
        }

        public DbSet<Veiculo> veiculos { get; set; }
        public DbSet<Sensor> sensores { get; set; }
        public DbSet<HistoricoSensor> historicoSensores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Veiculo>().ToTable("veiculos");
            modelBuilder.Entity<Sensor>().ToTable("sensores");
            modelBuilder.Entity<HistoricoSensor>().ToTable("historicoSensores");

            base.OnModelCreating(modelBuilder);
        }
    }
}