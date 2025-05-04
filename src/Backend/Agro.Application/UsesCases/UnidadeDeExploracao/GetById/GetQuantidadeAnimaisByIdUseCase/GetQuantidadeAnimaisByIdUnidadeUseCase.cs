using Agro.Communication.Response;
using Agro.Domain.Repositories.UnidadeDeExploracao;
using Agro.Exceptions;
using Agro.Exceptions.ExceptionsBase;

namespace Agro.Application.UsesCases.UnidadeDeExploracao.GetById.GetQuantidadeAnimaisByIdUseCase
{
    public class GetQuantidadeAnimaisByIdUnidadeUseCase(
        IUnidadeExploracaoReadOnlyRepository readOnlyRepository) : IGetQuantidadeAnimaisByIdUnidadeUseCase
    {
        private readonly IUnidadeExploracaoReadOnlyRepository _readOnlyRepository = readOnlyRepository;

        public async Task<ResponseQuantidadeAnimaisJson> Execute(int unidadeExploracaoId)
        {
            var unidadeExploracao = await _readOnlyRepository.GetById(unidadeExploracaoId);

            if (unidadeExploracao is null)
                throw new NotFoundException(ResourceMessagesException.EXPLORATION_UNIT_NOT_FOUND);

            return new ResponseQuantidadeAnimaisJson
            {
                QuantidadeAnimais = unidadeExploracao.QuantidadeAnimais
            };
        }
    }

}

