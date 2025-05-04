using Agro.Domain.Repositories.UnidadeDeExploracao;
using Moq;

namespace CommonTestUtilities.Repositories
{
    public class UnidadeExploracaoDeleteOnlyRepositoryBuilder
    {
        private readonly Mock<IUnidadeExploracaoDeleteOnlyRepository> _repository;

        public UnidadeExploracaoDeleteOnlyRepositoryBuilder()
        {
            _repository = new Mock<IUnidadeExploracaoDeleteOnlyRepository>();
        }

        public UnidadeExploracaoDeleteOnlyRepositoryBuilder WithUnidadeExploracaoExist()
        {
           _repository.Setup(r => r.Delete(It.IsAny<int>())).ReturnsAsync(true);
            return this;
        }

        public UnidadeExploracaoDeleteOnlyRepositoryBuilder WithUnidadeExploracaoInexist()
        {
            _repository.Setup(r => r.Delete(It.IsAny<int>())).ReturnsAsync(true);
            return this;
        }

        public IUnidadeExploracaoDeleteOnlyRepository Build() => _repository.Object;
    }
}
