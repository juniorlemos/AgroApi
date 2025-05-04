using Agro.Domain.Entities;
using Agro.Domain.Repositories.Especie;
using Agro.Domain.Repositories.UnidadeDeExploracao;
using Moq;

namespace CommonTestUtilities.Repositories
{
    public class UnidadeExploracaoReadOnlyRepositoryBuilder
    {
        private readonly Mock<IUnidadeExploracaoReadOnlyRepository> _repository;

        public UnidadeExploracaoReadOnlyRepositoryBuilder()
        {
            _repository = new Mock<IUnidadeExploracaoReadOnlyRepository>();
        }
        public UnidadeExploracaoReadOnlyRepositoryBuilder WithUnidadeExploracaoExist(UnidadeExploracao unidadeExploracao)
        {
            _repository.Setup(r => r.GetById(It.IsAny<int>())).ReturnsAsync(unidadeExploracao);
            return this;
        }

        public UnidadeExploracaoReadOnlyRepositoryBuilder WithUnidadeExploracaoInexist()
        {
            _repository.Setup(r => r.GetById(It.IsAny<int>())).ReturnsAsync((UnidadeExploracao)null);
            return this;
        }

        public UnidadeExploracaoReadOnlyRepositoryBuilder WithUnidadeExploracaoById(int id, UnidadeExploracao? unidade)
        {
            _repository.Setup(r => r.GetById(id)).ReturnsAsync(unidade);
            return this;
        }
        public UnidadeExploracaoReadOnlyRepositoryBuilder WithUnidadesExploracao(IEnumerable<UnidadeExploracao> unidades)
        {
            _repository.Setup(r => r.GetAll(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync((int page, int pageSize) =>
                {
                    if (page <= 0 || pageSize <= 0)
                        return unidades.ToList(); 

                    return unidades.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                });

            return this;
        }
        public IUnidadeExploracaoReadOnlyRepository Build() => _repository.Object;

    }
}
