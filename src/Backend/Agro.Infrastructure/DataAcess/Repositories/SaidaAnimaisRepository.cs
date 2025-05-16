using Agro.Domain.Entities;
using Agro.Domain.Repositories.SaidaAnimal;
using Microsoft.EntityFrameworkCore;

namespace Agro.Infrastructure.DataAcess.Repositories
{
    public sealed class SaidaAnimaisRepository(AgroDbContext dbContext) : ISaidaAnimaisWriteOnlyRepository, ISaidaAnimaisReadOnlyRepository, ISaidaAnimaisDeleteOnlyRepository,ISaidaAnimaisUpdateOnlyRepository
    {
        public async Task Add(SaidaAnimais saidaAnimais) => await dbContext.SaidasAnimais.AddAsync(saidaAnimais);

        public void Update(SaidaAnimais saidaAnimal) => dbContext.SaidasAnimais.Update(saidaAnimal);
        public async Task<bool> Delete(int saidaAnimaisId)
        {
            var saidaAnimais = await dbContext.SaidasAnimais.FindAsync(saidaAnimaisId);
            
            if (saidaAnimais == null)
                return false;

            dbContext.SaidasAnimais.Remove(saidaAnimais!);
            return true;
        }

        public async Task<IEnumerable<SaidaAnimais>> GetAll(int page, int pageSize)
        {
            var query = dbContext.SaidasAnimais.AsNoTracking().AsQueryable();

            if (page > 0 && pageSize > 0)
            {
                query = query
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize);
            }
            return await query.ToListAsync();
        }

        public async Task<SaidaAnimais?> GetById(int id)
        {
            return await dbContext
                .SaidasAnimais
                .FirstOrDefaultAsync(saidaAnimais => saidaAnimais.Id == id);
        }
    }
}

