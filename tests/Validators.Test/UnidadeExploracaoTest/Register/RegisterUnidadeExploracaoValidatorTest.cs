using Agro.Application.UsesCases.UnidadeDeExploracao.Register;
using Agro.Exceptions;
using CommonTestUtilities.Request;
using Shouldly;

namespace Validators.Test.UnidadeExploracaoTest.Register
{
    public class RegisterUnidadeExploracaoValidatorTest
    {
        [Fact]
        public void Success()
        {
            var validator = new RegisterUnidadeExploracaoValidator();

            var request = RequestRegisterUnidadeExploracaoJsonBuilder.Build();

            var result = validator.Validate(request);

            result.IsValid.ShouldBeTrue();
        }

    }
}
