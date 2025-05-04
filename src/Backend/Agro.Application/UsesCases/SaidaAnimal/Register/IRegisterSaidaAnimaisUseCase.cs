using Agro.Communication.Request;
using Agro.Communication.Response;

namespace Agro.Application.UsesCases.SaidaAnimal.Register
{
    public interface IRegisterSaidaAnimaisUseCase
    {
        public Task<ResponseRegisterSaidaAnimaisJson> Execute(RequestRegisterSaidaAnimaisJson request);
    }
}
