using Agro.Communication.Request;
using Agro.Communication.Response;

namespace Agro.Application.UsesCases.UnidadeDeExploracao.Register
{
    public interface IRegisterUnidadeExploracaoUseCase
    {
        public Task<ResponseRegisterUnidadeExploracaoJson> Execute(RequestRegisterUnidadeExploracaoJson request);
    }
}
