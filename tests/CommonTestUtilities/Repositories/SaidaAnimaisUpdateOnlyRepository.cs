using Agro.Domain.Repositories.SaidaAnimal;
using Moq;

namespace CommonTestUtilities.Repositories
{
    public class SaidaAnimaisUpdateOnlyRepository
    {
        public static ISaidaAnimaisUpdateOnlyRepository Build()
        {
            var mock = new Mock<ISaidaAnimaisUpdateOnlyRepository>();

            return mock.Object;
        }
    }
}
