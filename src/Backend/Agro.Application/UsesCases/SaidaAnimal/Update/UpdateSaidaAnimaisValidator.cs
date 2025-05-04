using Agro.Communication.Request;
using Agro.Exceptions;
using FluentValidation;

namespace Agro.Application.UsesCases.SaidaAnimal.Update
{
    public class UpdateSaidaAnimaisValidator : AbstractValidator<RequestUpdateSaidaAnimaisJson>
    {
        public UpdateSaidaAnimaisValidator()
        {
            RuleFor(saida => saida.DataSaida).NotNull().WithMessage(ResourceMessagesException.DEPARTURE_DATA_REQUIRED);
            RuleFor(saida => saida.QuantidadeAnimais).NotNull().WithMessage(ResourceMessagesException.QUANTITY_OF_ANIMALS_REQUIRED)
                .GreaterThan(0).WithMessage(ResourceMessagesException.ANIMALS_MUST_BE_ABOVE_ZERO);
        }
    }
}

