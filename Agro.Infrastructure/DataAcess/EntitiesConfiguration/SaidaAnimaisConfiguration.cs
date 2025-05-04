using Agro.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Agro.Infrastructure.DataAcess.EntitiesConfiguration
{
    public class SaidaAnimaisConfiguration : IEntityTypeConfiguration<SaidaAnimais>
    {
        public void Configure(EntityTypeBuilder<SaidaAnimais> builder)
        {
            builder.HasKey(sa => sa.Id);
            builder.Property(sa => sa.DataSaida).IsRequired();
            builder.Property(sa => sa.QuantidadeAnimais).IsRequired();

            builder.HasOne(sa => sa.UnidadeExploracaoOrigem)
                .WithMany(ue => ue.SaidasAnimaisOrigem)
                .HasForeignKey(sa => sa.CodigoUEPOrigem)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(sa => sa.UnidadeExploracaoDestino)
                .WithMany(ue => ue.SaidasAnimaisDestino)
                .HasForeignKey(sa => sa.CodigoUEPSaida)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
