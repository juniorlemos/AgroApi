namespace Agro.Domain.Repositories.Especie
{
    public interface IEspecieReadOnlyRepository
    {
        public Task<bool> ExistEspecie(int EspecieId);
    }
}
