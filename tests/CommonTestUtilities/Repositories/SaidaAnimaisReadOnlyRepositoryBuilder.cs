using Agro.Domain.Entities;
using Agro.Domain.Repositories.SaidaAnimal;
using Agro.Domain.Repositories.UnidadeDeExploracao;
using Moq;

namespace CommonTestUtilities.Repositories
{
    public class SaidaAnimaisReadOnlyRepositoryBuilder
    {
        private readonly Mock<ISaidaAnimaisReadOnlyRepository> _repository;

        public SaidaAnimaisReadOnlyRepositoryBuilder()
        {
            _repository = new Mock<ISaidaAnimaisReadOnlyRepository>();
        }
        public SaidaAnimaisReadOnlyRepositoryBuilder WithSaidaAnimaisExist(SaidaAnimais saidaAnimais)
        {
            _repository.Setup(r => r.GetById(It.IsAny<int>())).ReturnsAsync(saidaAnimais);
            return this;
        }

        public SaidaAnimaisReadOnlyRepositoryBuilder WithSaidaAnimaisInexist()
        {
            _repository.Setup(r => r.GetById(It.IsAny<int>())).ReturnsAsync((SaidaAnimais)null);
            return this;
        }
        public SaidaAnimaisReadOnlyRepositoryBuilder WithSaidasAnimaisExist(IEnumerable<SaidaAnimais> saidasAnimais)
        {
            _repository.Setup(r => r.GetAll(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync((int page, int pageSize) =>
                {
                    if (page <= 0 || pageSize <= 0)
                        return saidasAnimais.ToList();

                    return saidasAnimais.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                });

            return this;
        }
        public ISaidaAnimaisReadOnlyRepository Build() => _repository.Object;

    }
}

