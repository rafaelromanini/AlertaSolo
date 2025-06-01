using AlertaSolo.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AlertaSolo.Data.Mappings
{
    public class SensorMapping : IEntityTypeConfiguration<Sensor>
    {
        public void Configure(EntityTypeBuilder<Sensor> builder)
        {
            builder.ToTable("Sensor");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.CodigoEsp32).IsRequired().HasMaxLength(100);
            builder.Property(s => s.Status).IsRequired().HasMaxLength(20);
            builder.Property(s => s.TipoSensor).IsRequired().HasMaxLength(50);
            builder.Property(s => s.DataInstalacao).IsRequired();
            builder.Property(s => s.QntdAlertas).IsRequired();

            builder.HasOne(s => s.LocalRisco)
                   .WithMany(l => l.Sensores)
                   .HasForeignKey(s => s.LocalRiscoId);
        }
    }
}
