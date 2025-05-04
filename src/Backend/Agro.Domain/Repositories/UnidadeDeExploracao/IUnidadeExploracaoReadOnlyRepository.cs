using Agro.Domain.Entities;

namespace Agro.Domain.Repositories.UnidadeDeExploracao
{
    public interface IUnidadeExploracaoReadOnlyRepository
    {
        public Task<IEnumerable<UnidadeExploracao>> GetAll(int page, int pageSize);
        public Task<UnidadeExploracao?> GetById(int unidadeExploracao);
    }
}
