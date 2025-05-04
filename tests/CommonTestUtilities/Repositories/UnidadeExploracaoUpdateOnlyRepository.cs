using Agro.Domain.Repositories.UnidadeDeExploracao;
using Moq;

namespace CommonTestUtilities.Repositories
{
    public class UnidadeExploracaoUpdateOnlyRepository
    {
        public static IUnidadeExploracaoUpdateOnlyRepository Build()
        {
            var mock = new Mock<IUnidadeExploracaoUpdateOnlyRepository>();

            return mock.Object;
        }
    }
}
