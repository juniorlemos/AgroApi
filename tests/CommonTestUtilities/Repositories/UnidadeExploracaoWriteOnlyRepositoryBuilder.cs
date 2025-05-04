using Agro.Domain.Repositories;
using Agro.Domain.Repositories.UnidadeDeExploracao;
using Moq;

namespace CommonTestUtilities.Repositories
{
    public class UnidadeExploracaoWriteOnlyRepositoryBuilder
    {
        public static IUnidadeExploracaoWriteOnlyRepository Build()
        {
            var mock = new Mock<IUnidadeExploracaoWriteOnlyRepository>();

            return mock.Object;
        }
    }
}
