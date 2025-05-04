using Agro.Communication.Response;
using Agro.Domain.Repositories.SaidaAnimal;
using Mapster;

namespace Agro.Application.UsesCases.SaidaAnimal.GetAll
{
    public class GetAllSaidaAnimaisUseCase(ISaidaAnimaisReadOnlyRepository readOnlyRepository)
        : IGetAllSaidaAnimaisUseCase
    {
        private readonly ISaidaAnimaisReadOnlyRepository _readOnlyRepository = readOnlyRepository;

      public async Task<IEnumerable<ResponseSaidaAnimaisJson>> Execute(int page, int pageSize)
        {
            var unidadeExploracao = await _readOnlyRepository.GetAll(page, pageSize);

            var response = unidadeExploracao.Adapt<IEnumerable<ResponseSaidaAnimaisJson>>();

            return response;
        }
    }
}
  
