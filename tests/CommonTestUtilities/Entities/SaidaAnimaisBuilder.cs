using Agro.Domain.Entities;
using Bogus;

namespace CommonTestUtilities.Entities
{
    public class SaidaAnimaisBuilder
    {
        public static SaidaAnimais Build()
        {

            var unidadeOrigem = UnidadeExploracaoBuilder.Build(); 
            var unidadeDestino = UnidadeExploracaoBuilder.Build(); 

            return new Faker<SaidaAnimais>()
                .RuleFor(sa => sa.Id, f => f.Random.Int(1, 1000))
                .RuleFor(sa => sa.DataSaida, f => f.Date.Past(1))
                .RuleFor(sa => sa.QuantidadeAnimais, f => f.Random.Int(1, 500))
                .RuleFor(sa => sa.CodigoUEPOrigem, f => unidadeOrigem.Id)
                .RuleFor(sa => sa.CodigoUEPSaida, f => unidadeDestino.Id)
                .RuleFor(sa => sa.UnidadeExploracaoOrigem, _ => unidadeOrigem)
                .RuleFor(sa => sa.UnidadeExploracaoDestino, _ => unidadeDestino);
        }
    }
}