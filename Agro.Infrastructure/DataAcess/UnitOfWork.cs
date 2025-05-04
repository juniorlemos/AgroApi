using Agro.Domain.Repositories;

namespace Agro.Infrastructure.DataAcess
{
    public class UnitOfWork(AgroDbContext dbContext) : IUnitOfWork
    {
        public async Task Commit() => await dbContext.SaveChangesAsync();
    }
}
