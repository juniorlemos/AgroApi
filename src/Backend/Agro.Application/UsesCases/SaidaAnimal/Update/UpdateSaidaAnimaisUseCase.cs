using Agro.Communication.Request;
using Agro.Domain.Extensions;
using Agro.Domain.Repositories;
using Agro.Domain.Repositories.SaidaAnimal;
using Agro.Domain.Repositories.UnidadeDeExploracao;
using Agro.Exceptions;
using Agro.Exceptions.ExceptionsBase;

namespace Agro.Application.UsesCases.SaidaAnimal.Update
{
    public class UpdateSaidaAnimaisUseCase : IUpdateSaidaAnimaisUseCase
    {
        private readonly ISaidaAnimaisUpdateOnlyRepository _repositoryUpdateOnly;
        private readonly ISaidaAnimaisReadOnlyRepository _saidaAnimalRepositoryReadOnly;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUnidadeExploracaoReadOnlyRepository _unidadeExploracaoReadOnlyRepository;
        private readonly IUnidadeExploracaoUpdateOnlyRepository _unidadeExploracaoUpdateOnlyRepository;
        public UpdateSaidaAnimaisUseCase(
               ISaidaAnimaisUpdateOnlyRepository repositoryUpdateOnly,
               IUnitOfWork unitOfWork,
               ISaidaAnimaisReadOnlyRepository saidaAnimalRepositoryReadOnly,
               IUnidadeExploracaoReadOnlyRepository unidadeExploracaoReadOnlyRepository,
               IUnidadeExploracaoUpdateOnlyRepository unidadeExploracaoUpdateOnlyRepository)
        {
            _repositoryUpdateOnly = repositoryUpdateOnly;
            _unitOfWork = unitOfWork;
            _saidaAnimalRepositoryReadOnly = saidaAnimalRepositoryReadOnly;
            _unidadeExploracaoReadOnlyRepository = unidadeExploracaoReadOnlyRepository;
            _unidadeExploracaoUpdateOnlyRepository = unidadeExploracaoUpdateOnlyRepository;
        }

        public async Task Execute(int saidaAnimaisId,RequestUpdateSaidaAnimaisJson request)
        {
            await Validate(request);
            
            var saidaAnimais = await _saidaAnimalRepositoryReadOnly.GetById(saidaAnimaisId);

            if (saidaAnimais is null)
                throw new NotFoundException(ResourceMessagesException.ANIMAL_EXIT_NOT_FOUND);

            var unidadeExploracaoOrigem = await _unidadeExploracaoReadOnlyRepository.GetById(saidaAnimais.CodigoUEPOrigem);
            
            if (unidadeExploracaoOrigem is null)
                throw new NotFoundException(ResourceMessagesException.EXPLORATION_UNIT_NOT_FOUND);

            var unidadeExploracaoSaida = await _unidadeExploracaoReadOnlyRepository.GetById(saidaAnimais.CodigoUEPSaida);

            if (unidadeExploracaoSaida is null)
                throw new NotFoundException(ResourceMessagesException.EXPLORATION_UNIT_NOT_FOUND);
            
            if (request.QuantidadeAnimais > saidaAnimais.QuantidadeAnimais)
            {
                if (request.QuantidadeAnimais > unidadeExploracaoOrigem.QuantidadeAnimais)
                    throw new BusinessException(ResourceMessagesException.QUANTITY_EXCEEDS_AVAILABLE_SOURCE_ANIMALS);

                unidadeExploracaoOrigem.QuantidadeAnimais -= (request.QuantidadeAnimais - saidaAnimais.QuantidadeAnimais);
                unidadeExploracaoSaida.QuantidadeAnimais +=  (request.QuantidadeAnimais - saidaAnimais.QuantidadeAnimais);
            }            

            if (request.QuantidadeAnimais < saidaAnimais.QuantidadeAnimais)
            {
                if (request.QuantidadeAnimais > unidadeExploracaoSaida.QuantidadeAnimais)
                    throw new BusinessException(ResourceMessagesException.QUANTITY_EXCEEDS_AVAILABLE_SOURCE_ANIMALS);

                unidadeExploracaoOrigem.QuantidadeAnimais += (saidaAnimais.QuantidadeAnimais - request.QuantidadeAnimais);
                unidadeExploracaoSaida.QuantidadeAnimais -= (saidaAnimais.QuantidadeAnimais - request.QuantidadeAnimais);
            }

            _unidadeExploracaoUpdateOnlyRepository.Update(unidadeExploracaoOrigem);

            _unidadeExploracaoUpdateOnlyRepository.Update(unidadeExploracaoSaida);

            saidaAnimais.DataSaida = request.DataSaida;
            saidaAnimais.QuantidadeAnimais = request.QuantidadeAnimais;

            _repositoryUpdateOnly.Update(saidaAnimais);

            await _unitOfWork.Commit();
        }

        private static async Task Validate(RequestUpdateSaidaAnimaisJson request)
        {
            var validator = new UpdateSaidaAnimaisValidator();

            var result = await validator.ValidateAsync(request);

            if (result.IsValid.IsFalse())
            {
                var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
