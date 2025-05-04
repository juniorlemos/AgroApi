using Agro.Domain.Entities;
using Agro.Domain.Repositories.UnidadeDeExploracao;
using Microsoft.EntityFrameworkCore;

namespace Agro.Infrastructure.DataAcess.Repositories
{
    public sealed class UnidadeExploracaoRepository(AgroDbContext dbContext) : IUnidadeExploracaoWriteOnlyRepository, IUnidadeExploracaoReadOnlyRepository, IUnidadeExploracaoDeleteOnlyRepository, IUnidadeExploracaoUpdateOnlyRepository
    {
        public async Task Add(UnidadeExploracao unidadeExploracao) => await dbContext.UnidadesExploracoes.AddAsync(unidadeExploracao);
        public void Update(UnidadeExploracao unidadeExploracao) => dbContext.UnidadesExploracoes.Update(unidadeExploracao);

        public async Task<bool> Delete(int unidadeExploracaoId)
        {
            var unidadeExploracao = await dbContext.UnidadesExploracoes.FindAsync(unidadeExploracaoId);

            if (unidadeExploracao == null)
                return false;

            dbContext.UnidadesExploracoes.Remove(unidadeExploracao!);

            return true;
        }    
        public async Task<IEnumerable<UnidadeExploracao>> GetAll(int page, int pageSize)
        {
            var query = dbContext.UnidadesExploracoes.AsNoTracking().AsQueryable();

            if (page > 0 && pageSize > 0)
            {
                query = query
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize);
            }
            return await query.ToListAsync();
        }

        public async Task<UnidadeExploracao?> GetById(int id)
        {
            return await dbContext
                .UnidadesExploracoes
                .FirstOrDefaultAsync(unidadeExploracao => unidadeExploracao.Id == id);
        }
    }
}
