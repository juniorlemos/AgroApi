using Agro.Domain.Repositories.SaidaAnimal;
using Moq;

namespace CommonTestUtilities.Repositories
{
    public class SaidaAnimaisDeleteOnlyRepositoryBuilder
    {
        private readonly Mock<ISaidaAnimaisDeleteOnlyRepository> _repository;

        public SaidaAnimaisDeleteOnlyRepositoryBuilder()
        {
            _repository = new Mock<ISaidaAnimaisDeleteOnlyRepository>();
        }

        public SaidaAnimaisDeleteOnlyRepositoryBuilder WithSaidaAnimaisExist()
        {
            _repository.Setup(r => r.Delete(It.IsAny<int>())).ReturnsAsync(true);
            return this;
        }

        public SaidaAnimaisDeleteOnlyRepositoryBuilder WithSaidaAnimaisInexist()
        {
            _repository.Setup(r => r.Delete(It.IsAny<int>())).ReturnsAsync(true);
            return this;
        }

        public ISaidaAnimaisDeleteOnlyRepository Build() => _repository.Object;
    }
}
