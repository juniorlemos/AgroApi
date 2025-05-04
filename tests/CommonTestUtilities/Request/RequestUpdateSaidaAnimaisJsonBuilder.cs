using Agro.Communication.Request;
using Bogus;

namespace CommonTestUtilities.Request
{
    public class RequestUpdateSaidaAnimaisJsonBuilder
    {
        public static RequestUpdateSaidaAnimaisJson Build()
        {
            return new Faker<RequestUpdateSaidaAnimaisJson>()
            .RuleFor(saida => saida.DataSaida, f => f.Date.Past(1))
            .RuleFor(unidade => unidade.QuantidadeAnimais, f => f.Random.Int(1, 500));
        }
    }
}
