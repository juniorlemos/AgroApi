using Agro.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Agro.Infrastructure.DataAcess.EntitiesConfiguration
{
    public class UnidadeExploracaoConfiguration : IEntityTypeConfiguration<UnidadeExploracao>
    {
        public void Configure(EntityTypeBuilder<UnidadeExploracao> builder)
        {
            builder.HasKey(ue => ue.Id);
            builder.Property(ue => ue.CodigoPropriedade).IsRequired();
            builder.Property(ue => ue.QuantidadeAnimais).IsRequired();

            builder.HasOne(ue => ue.Especie)              
                   .WithMany()                            
                   .HasForeignKey(ue => ue.EspecieId)      
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}