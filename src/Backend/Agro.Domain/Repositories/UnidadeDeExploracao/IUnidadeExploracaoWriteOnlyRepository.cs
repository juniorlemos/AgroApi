using Agro.Domain.Entities;

namespace Agro.Domain.Repositories.UnidadeDeExploracao
{
    public interface IUnidadeExploracaoWriteOnlyRepository
    {
        public Task Add(UnidadeExploracao unidadeExploracao);
    }
}
