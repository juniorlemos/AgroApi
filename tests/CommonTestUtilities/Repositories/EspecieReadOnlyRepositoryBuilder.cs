using Agro.Domain.Repositories.Especie;
using Moq;

namespace CommonTestUtilities.Repositories
{
    public class EspecieReadOnlyRepositoryBuilder
    {
        private readonly Mock<IEspecieReadOnlyRepository> _repository;

        public EspecieReadOnlyRepositoryBuilder()
        {
            _repository = new Mock<IEspecieReadOnlyRepository>();
        }

        
        public EspecieReadOnlyRepositoryBuilder WithEspecieExist()
        {
            _repository.Setup(r => r.ExistEspecie(It.IsAny<int>())).ReturnsAsync(true);
            return this;
        }
 
        public EspecieReadOnlyRepositoryBuilder WithEspecieInexist()
        {
            _repository.Setup(r => r.ExistEspecie(It.IsAny<int>())).ReturnsAsync(false);
            return this;
        }

        public IEspecieReadOnlyRepository Build() => _repository.Object;
    }
}
