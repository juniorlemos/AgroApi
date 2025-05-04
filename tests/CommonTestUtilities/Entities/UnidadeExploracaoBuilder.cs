using Agro.Domain.Entities;
using Bogus;

namespace CommonTestUtilities.Entities
{

    public class UnidadeExploracaoBuilder
    {
        public static UnidadeExploracao Build()
        {
            var faker = new Faker();

            var especies = EspecieBuilder.Build();

            var especie = faker.Random.ListItem(especies);

            return new Faker<UnidadeExploracao>()
                .RuleFor(ue => ue.Id, f => f.Random.Int(1, 1000))
                .RuleFor(ue => ue.QuantidadeAnimais, f => f.Random.Int(50, 500))
                .RuleFor(ue => ue.CodigoPropriedade, f => f.Random.Int(1000, 9999))
                .RuleFor(ue => ue.EspecieId, f => especie.Id) 
                .RuleFor(ue => ue.Especie, _ => especie)
                .RuleFor(ue => ue.SaidasAnimaisOrigem, _ => [])
                .RuleFor(ue => ue.SaidasAnimaisDestino, _ => []);
        }
    }
}