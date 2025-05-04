using Agro.Communication.Request;

namespace Agro.Application.UsesCases.SaidaAnimal.Update
{
    public interface IUpdateSaidaAnimaisUseCase
    {
        public Task Execute(int id, RequestUpdateSaidaAnimaisJson request);
    }
}
