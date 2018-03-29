using Buzy.DataAccess.Model;
using Microsoft.EntityFrameworkCore;

namespace Buzy.DataAccess
{
    public class BusHelperContext : DbContext
    {
        public BusHelperContext(DbContextOptions<BusHelperContext> options) : base(options)
        {
        }

        public DbSet<Veiculo> Veiculo { get; set; }
        public DbSet<Sensor> Sensores { get; set; }
        public DbSet<HistoricoSensor> HistoricoSensores { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<PontoDeOnibus> PontosDeOnibus { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Veiculo>().ToTable("veiculos");
            modelBuilder.Entity<Sensor>().ToTable("sensores");
            modelBuilder.Entity<HistoricoSensor>().ToTable("historicoSensores");
            modelBuilder.Entity<Usuario>().ToTable("usuarios");
            modelBuilder.Entity<PontoDeOnibus>().ToTable("pontosDeOnibus");
        }
    }
}