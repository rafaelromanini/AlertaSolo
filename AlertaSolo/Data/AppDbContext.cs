using AlertaSolo.Data.Mappings;
using AlertaSolo.Model;
using Microsoft.EntityFrameworkCore;

namespace AlertaSolo.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<LocalRisco> LocaisRisco { get; set; }
        public DbSet<Sensor> Sensores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioMapping());
            modelBuilder.ApplyConfiguration(new LocalRiscoMapping());
            modelBuilder.ApplyConfiguration(new SensorMapping());
        }
    }
}
