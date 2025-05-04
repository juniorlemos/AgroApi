using Agro.Communication.Response;

namespace Agro.Application.UsesCases.UnidadeDeExploracao.GetById.GetUnidadeExploracaoByIdUseCase
{
    public interface IGetUnidadeExploracaoByIdUseCase
    {
        Task<ResponseUnidadeExploracaoJson> Execute(int unidadeExploracaoId);
    }
}
