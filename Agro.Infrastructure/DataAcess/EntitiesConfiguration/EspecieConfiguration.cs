using Agro.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Agro.Infrastructure.DataAcess.EntitiesConfiguration
{
    public class EspecieConfiguration : IEntityTypeConfiguration<Especie>
    {
        public void Configure(EntityTypeBuilder<Especie> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.NomeEspecie)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
