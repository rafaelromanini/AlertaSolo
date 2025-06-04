using AlertaSolo.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AlertaSolo.Data.Mappings
{
    public class LocalRiscoMapping : IEntityTypeConfiguration<LocalRisco>
    {
        public void Configure(EntityTypeBuilder<LocalRisco> builder)
        {
            builder.ToTable("LocalRisco");

            builder.HasKey(l => l.Id);

            builder.Property(l => l.NomeLocal).IsRequired().HasMaxLength(100);
            builder.Property(l => l.Cidade).IsRequired().HasMaxLength(50);
            builder.Property(l => l.Uf).IsRequired().HasMaxLength(2);
            builder.Property(l => l.GrauRisco).IsRequired();
            builder.Property(l => l.Latitude).HasMaxLength(50);
            builder.Property(l => l.Longitude).HasMaxLength(50);

            builder.Property(l => l.Ativo)
                   .IsRequired()
                   .HasConversion<int>();
        }
    }
}
