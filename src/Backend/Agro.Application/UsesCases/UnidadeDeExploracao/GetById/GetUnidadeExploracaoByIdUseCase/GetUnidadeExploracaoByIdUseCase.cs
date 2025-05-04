using Agro.Communication.Response;
using Agro.Domain.Repositories.UnidadeDeExploracao;
using Agro.Exceptions;
using Agro.Exceptions.ExceptionsBase;
using Mapster;

namespace Agro.Application.UsesCases.UnidadeDeExploracao.GetById.GetUnidadeExploracaoByIdUseCase
{
    public class GetUnidadeExploracaoByIdUseCase(
        IUnidadeExploracaoReadOnlyRepository readOnlyRepository) : IGetUnidadeExploracaoByIdUseCase
    {
        private readonly IUnidadeExploracaoReadOnlyRepository _readOnlyRepository = readOnlyRepository;

        public async Task<ResponseUnidadeExploracaoJson> Execute(int unidadeExploracaoId)
        {
            var unidadeExploracao = await _readOnlyRepository.GetById(unidadeExploracaoId);

            if (unidadeExploracao is null)
                throw new NotFoundException(ResourceMessagesException.EXPLORATION_UNIT_NOT_FOUND);

            return unidadeExploracao.Adapt<ResponseUnidadeExploracaoJson>();
        }
    }

}

