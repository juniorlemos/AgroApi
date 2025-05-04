namespace Agro.Domain.Repositories
{
    public interface IUnitOfWork
    {
        public Task Commit();
    }
}
