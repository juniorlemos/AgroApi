using Agro.Communication.Request;
using Agro.Exceptions;
using FluentValidation;

namespace Agro.Application.UsesCases.SaidaAnimal.Register
{
    public class RegisterSaidaAnimaisValidator : AbstractValidator<RequestRegisterSaidaAnimaisJson>
    {
        public RegisterSaidaAnimaisValidator()
        {
            RuleFor(saida => saida.DataSaida).NotNull().WithMessage(ResourceMessagesException.DEPARTURE_DATA_REQUIRED);
            RuleFor(saida => saida.QuantidadeAnimais).NotNull().WithMessage(ResourceMessagesException.QUANTITY_OF_ANIMALS_REQUIRED)
                .GreaterThan(0).WithMessage(ResourceMessagesException.ANIMALS_MUST_BE_ABOVE_ZERO); ;
            RuleFor(saida => saida.CodigoUEPOrigem).NotNull().WithMessage(ResourceMessagesException.UEP_ORIGIN_REQUIRED);
            RuleFor(saida => saida.CodigoUEPSaida).NotNull().WithMessage(ResourceMessagesException.UEP_DEPARTURE_REQUIRED);
        }

    }
}