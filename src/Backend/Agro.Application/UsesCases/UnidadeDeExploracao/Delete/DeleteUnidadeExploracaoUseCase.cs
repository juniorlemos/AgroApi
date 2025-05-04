using Agro.Domain.Entities;
using Agro.Domain.Extensions;
using Agro.Domain.Repositories;
using Agro.Domain.Repositories.UnidadeDeExploracao;
using Agro.Exceptions;
using Agro.Exceptions.ExceptionsBase;

namespace Agro.Application.UsesCases.UnidadeDeExploracao.Delete
{
    public class DeleteUnidadeExploracaoUseCase(
        IUnidadeExploracaoDeleteOnlyRepository deleteOnlyRepository,
        IUnidadeExploracaoReadOnlyRepository readOnlyRepository,
        IUnitOfWork unitOfWork) : IDeleteUnidadeExploracaoUseCase
    {
        private readonly IUnidadeExploracaoReadOnlyRepository _readOnlyRepository = readOnlyRepository;
        private readonly IUnidadeExploracaoDeleteOnlyRepository _deleteOnlyRepository = deleteOnlyRepository;

        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task Execute(int unidadeExploracaoId)
        {
            var unidadeExploracao = await _readOnlyRepository.GetById(unidadeExploracaoId);

            if (unidadeExploracao is null)
                throw new NotFoundException(ResourceMessagesException.EXPLORATION_UNIT_NOT_FOUND);

            if (unidadeExploracao.QuantidadeAnimais > 0)
                throw new NotFoundException(ResourceMessagesException.EXPLORATION_UNIT_CANNOT_BE_DELETED_WITH_ANIMALS);


            await _deleteOnlyRepository.Delete(unidadeExploracao.Id);

         
            await _unitOfWork.Commit();
        }
    }
}