using Agro.Domain.Repositories;
using Agro.Domain.Repositories.SaidaAnimal;
using Agro.Domain.Repositories.UnidadeDeExploracao;
using Agro.Exceptions;
using Agro.Exceptions.ExceptionsBase;

namespace Agro.Application.UsesCases.SaidaAnimal.Delete
{
    public class DeleteSaidaAnimaisUseCase(
        ISaidaAnimaisDeleteOnlyRepository deleteOnlyRepository,
        IUnitOfWork unitOfWork,
        IUnidadeExploracaoReadOnlyRepository unidadeExploracaoReadOnlyRepository,
        ISaidaAnimaisReadOnlyRepository saidaAnimaisReadOnlyRepository,
        IUnidadeExploracaoUpdateOnlyRepository unidadeExploracaoUpdateOnlyRepository) : IDeleteSaidaAnimaisUseCase
    {
        private readonly ISaidaAnimaisDeleteOnlyRepository _deleteOnlyRepository = deleteOnlyRepository;
        private readonly ISaidaAnimaisReadOnlyRepository _saidaAnimaisReadOnlyRepository = saidaAnimaisReadOnlyRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IUnidadeExploracaoUpdateOnlyRepository _unidadeExploracaoUpdateOnlyRepository = unidadeExploracaoUpdateOnlyRepository;
        private readonly IUnidadeExploracaoReadOnlyRepository _unidadeExploracaoReadOnlyRepository = unidadeExploracaoReadOnlyRepository;
        public async Task Execute(int saidaAnimaisId)
        {

            var saidaAnimais = await _saidaAnimaisReadOnlyRepository.GetById(saidaAnimaisId);
          
            if (saidaAnimais == null)
                throw new NotFoundException(ResourceMessagesException.ANIMAL_EXIT_NOT_FOUND);
          
            var unidadeExploracaoOrigem = await _unidadeExploracaoReadOnlyRepository.GetById(saidaAnimais.CodigoUEPOrigem);

            if (unidadeExploracaoOrigem is null)
                throw new NotFoundException(ResourceMessagesException.EXPLORATION_UNIT_NOT_FOUND);

            var unidadeExploracaoSaida = await _unidadeExploracaoReadOnlyRepository.GetById(saidaAnimais.CodigoUEPSaida);

            if (unidadeExploracaoSaida is null)
                throw new NotFoundException(ResourceMessagesException.EXPLORATION_UNIT_NOT_FOUND);

            if (saidaAnimais.QuantidadeAnimais > unidadeExploracaoSaida.QuantidadeAnimais)
                throw new BusinessException(ResourceMessagesException.QUANTITY_EXCEEDS_AVAILABLE_SOURCE_ANIMALS);

            unidadeExploracaoOrigem.QuantidadeAnimais += saidaAnimais.QuantidadeAnimais;

            unidadeExploracaoSaida.QuantidadeAnimais -= saidaAnimais.QuantidadeAnimais;

            _unidadeExploracaoUpdateOnlyRepository.Update(unidadeExploracaoOrigem);
            _unidadeExploracaoUpdateOnlyRepository.Update(unidadeExploracaoSaida);

            await _deleteOnlyRepository.Delete(saidaAnimaisId);
         
            await _unitOfWork.Commit();
        }
    }
}