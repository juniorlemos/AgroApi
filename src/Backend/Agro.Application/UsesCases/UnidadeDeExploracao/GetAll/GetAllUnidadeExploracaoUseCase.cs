using Agro.Communication.Response;
using Agro.Domain.Repositories.UnidadeDeExploracao;
using Mapster;
using MapsterMapper;

namespace Agro.Application.UsesCases.UnidadeDeExploracao.GetAll
{
    public class GetAllUnidadeExploracaoUseCase(IUnidadeExploracaoReadOnlyRepository readOnlyRepository)
        : IGetAllUnidadeExploracaoUseCase
    {
        private readonly IUnidadeExploracaoReadOnlyRepository _readOnlyRepository = readOnlyRepository;

        public async Task<IEnumerable<ResponseUnidadeExploracaoJson>> Execute(int page, int pageSize)
        {
            var unidadeExploracao = await _readOnlyRepository.GetAll(page, pageSize);

            var response = unidadeExploracao.Adapt<IEnumerable<ResponseUnidadeExploracaoJson>>();

            return response;
        }
    }
}
  
