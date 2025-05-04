using Agro.Communication.Response;

namespace Agro.Application.UsesCases.UnidadeDeExploracao.GetById.GetQuantidadeAnimaisByIdUseCase
{
    public interface IGetQuantidadeAnimaisByIdUnidadeUseCase
    {
        Task<ResponseQuantidadeAnimaisJson> Execute(int unidadeExploracaoId);
    }
}
