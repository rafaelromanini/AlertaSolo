using AlertaSolo.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AlertaSolo.Data.Mappings
{
    public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuario");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Nome).IsRequired().HasMaxLength(100);
            builder.Property(u => u.Cpf).IsRequired().HasMaxLength(14);
            builder.Property(u => u.Idade).IsRequired();
            builder.Property(u => u.Cidade).IsRequired().HasMaxLength(50);
            builder.Property(u => u.Uf).IsRequired().HasMaxLength(2);
            builder.Property(u => u.Email).IsRequired().HasMaxLength(100);
            builder.Property(u => u.Senha).IsRequired().HasMaxLength(100);
            builder.Property(u => u.DataCadastro).IsRequired();
        }
    }
}
