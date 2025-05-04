namespace Agro.Domain.Repositories.SaidaAnimal
{
    public interface ISaidaAnimaisDeleteOnlyRepository
    {
        public Task<bool> Delete(int saidaAnimaisId);
    }
}
