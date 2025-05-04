using Agro.Domain.Entities;

namespace Agro.Domain.Repositories.SaidaAnimal
{
    public interface ISaidaAnimaisWriteOnlyRepository
    {
        public Task Add(SaidaAnimais saidaAnimais);
    }
}
