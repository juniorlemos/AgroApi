using Agro.Communication.Response;

namespace Agro.Application.UsesCases.SaidaAnimal.GetById
{
    public interface IGetSaidaAnimaisByIdUseCase
    {
        Task<ResponseSaidaAnimaisJson> Execute(int saidaAnimaisId);
    }
}