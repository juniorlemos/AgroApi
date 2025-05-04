using Agro.Communication.Response;

namespace Agro.Application.UsesCases.UnidadeDeExploracao.GetAll
{
    public interface IGetAllUnidadeExploracaoUseCase
    {
        Task<IEnumerable<ResponseUnidadeExploracaoJson>> Execute(int page, int pagesize);
    }
}
