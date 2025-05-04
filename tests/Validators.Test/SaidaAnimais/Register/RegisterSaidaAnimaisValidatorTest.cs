using Agro.Application.UsesCases.SaidaAnimal.Register;
using Agro.Application.UsesCases.UnidadeDeExploracao.Register;
using Agro.Exceptions;
using CommonTestUtilities.Request;
using Shouldly;

namespace Validators.Test.SaidaAnimais.Register
{
    public class RegisterSaidaAnimaisValidatorTest
    {
        [Fact]
        public void Success()
        {
            var validator = new RegisterSaidaAnimaisValidator();

            var request = RequestRegisterSaidaAnimaisJsonBuilder.Build();

            var result = validator.Validate(request);

            result.IsValid.ShouldBeTrue();
        }

        [Fact]
        public void Error_QuantidadeAnimais_Is_Zero()
        {
            var validator = new RegisterSaidaAnimaisValidator();

            var request = RequestRegisterSaidaAnimaisJsonBuilder.Build();
            request.QuantidadeAnimais = 0;

            var result = validator.Validate(request);

            result.IsValid.ShouldBeFalse();

            result.Errors.ShouldContain(e => e.ErrorMessage == ResourceMessagesException.ANIMALS_MUST_BE_ABOVE_ZERO);
            result.Errors.Count.ShouldBe(1);
        }
    }
}
