

namespace Agro.Domain.Entities
{
    public class UnidadeExploracao : EntityBase
    {
        public int QuantidadeAnimais { get; set; }
        public int CodigoPropriedade { get; set; }
        public int EspecieId { get; set; }         

        public Especie Especie { get; set; }
        public List<SaidaAnimais> SaidasAnimaisOrigem { get; set; }
        public List<SaidaAnimais> SaidasAnimaisDestino { get; set; }

    }
}
