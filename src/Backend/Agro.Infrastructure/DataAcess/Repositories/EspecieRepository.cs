using Agro.Domain.Repositories.Especie;
using Microsoft.EntityFrameworkCore;

namespace Agro.Infrastructure.DataAcess.Repositories
{
    public sealed class EspecieRepository(AgroDbContext dbContext) : IEspecieReadOnlyRepository
    {
        public async Task<bool> ExistEspecie(int especieId) => await dbContext.Especies.AnyAsync(especie => especie.Id == especieId);
    }
}
