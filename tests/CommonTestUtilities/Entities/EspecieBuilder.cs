using Agro.Domain.Entities;
using Bogus;

namespace CommonTestUtilities.Entities
{
    public class EspecieBuilder
    {
        public static List<Especie> Build()
        {
            return new List<Especie>
        {
            new Especie { Id = 1, NomeEspecie = "Bovino" },
            new Especie { Id = 2, NomeEspecie = "Suíno" },
            new Especie { Id = 3, NomeEspecie = "Caprino" },
            new Especie { Id = 4, NomeEspecie = "Ovino" }
        };
        }
    }
}
