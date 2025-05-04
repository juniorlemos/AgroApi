using Agro.Communication.Request;
using Agro.Exceptions;
using FluentValidation;

namespace Agro.Application.UsesCases.UnidadeDeExploracao.Register
{
    public class RegisterUnidadeExploracaoValidator : AbstractValidator<RequestRegisterUnidadeExploracaoJson>
    {
        public RegisterUnidadeExploracaoValidator( )
        {

            RuleFor(unidade => unidade.QuantidadeAnimais).NotNull().WithMessage(ResourceMessagesException.QUANTITY_OF_ANIMALS_REQUIRED);
            RuleFor(unidade => unidade.CodigoPropriedade).NotNull().WithMessage(ResourceMessagesException.PROPERTY_CODE_REQUIRED);
            RuleFor(unidade => unidade.CodigoEspecie).NotNull().WithMessage(ResourceMessagesException.SPECIES_CODE_REQUIRED);
        }
    }
}
