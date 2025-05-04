using Agro.Communication.Request;
using Bogus;

namespace CommonTestUtilities.Request
{
    public class RequestRegisterSaidaAnimaisJsonBuilder
    {
        public static RequestRegisterSaidaAnimaisJson Build()
        {
            return new Faker<RequestRegisterSaidaAnimaisJson>()
            .RuleFor(saida => saida.DataSaida, f => f.Date.Past(1))
            .RuleFor(saida => saida.QuantidadeAnimais, f => f.Random.Int(1, 500))
            .RuleFor(saida => saida.CodigoUEPOrigem, f => f.Random.Int(1, 10))
            .RuleFor(saida => saida.CodigoUEPSaida, f => f.Random.Int(1, 10));
        }
    }
}
