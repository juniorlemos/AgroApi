using Agro.Domain.Entities;

namespace Agro.Domain.Repositories.SaidaAnimal
{
    public interface ISaidaAnimaisReadOnlyRepository
    {
        public Task<IEnumerable<SaidaAnimais>> GetAll(int page, int pageSize);
        public Task<SaidaAnimais?> GetById(int saidaAnimaisId);
    }
}
