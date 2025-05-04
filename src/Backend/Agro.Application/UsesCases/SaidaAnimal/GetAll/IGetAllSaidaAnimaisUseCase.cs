using Agro.Communication.Response;

namespace Agro.Application.UsesCases.SaidaAnimal.GetAll
{
    public interface IGetAllSaidaAnimaisUseCase
    {
        Task<IEnumerable<ResponseSaidaAnimaisJson>> Execute(int page, int pagesize);
    }
}
