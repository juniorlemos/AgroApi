namespace Agro.Domain.Repositories.UnidadeDeExploracao
{
    public interface IUnidadeExploracaoDeleteOnlyRepository
    {
        public Task<bool> Delete(int unidadeExploracao);
    }
}
