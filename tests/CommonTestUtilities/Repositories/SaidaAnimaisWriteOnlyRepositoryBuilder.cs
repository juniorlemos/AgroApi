using Agro.Domain.Repositories.SaidaAnimal;
using Agro.Domain.Repositories.UnidadeDeExploracao;
using Moq;

namespace CommonTestUtilities.Repositories
{
    public class SaidaAnimaisWriteOnlyRepositoryBuilder
    {
        public static ISaidaAnimaisWriteOnlyRepository Build()
        {
            var mock = new Mock<ISaidaAnimaisWriteOnlyRepository>();

            return mock.Object;
        }
    }
}
