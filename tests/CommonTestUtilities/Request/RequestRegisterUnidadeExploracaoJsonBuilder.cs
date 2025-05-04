using Agro.Communication.Request;
using Bogus;

namespace CommonTestUtilities.Request
{
    public class RequestRegisterUnidadeExploracaoJsonBuilder
    {
        public static RequestRegisterUnidadeExploracaoJson Build()
        {
            return new Faker<RequestRegisterUnidadeExploracaoJson>()
            .RuleFor(unidade => unidade.QuantidadeAnimais,f => f.Random.Int(1, 500))
            .RuleFor(unidade => unidade.CodigoPropriedade,f => f.Random.Int(1000, 9999))
            .RuleFor(unidade => unidade.CodigoEspecie, f => f.Random.Int(1, 10));
        }
    }
}
