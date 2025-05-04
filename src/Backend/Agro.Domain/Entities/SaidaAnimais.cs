

namespace Agro.Domain.Entities
{
    public class SaidaAnimais : EntityBase
    {
        public DateTime DataSaida { get; set; }
        public int QuantidadeAnimais { get; set; }
        public int CodigoUEPOrigem { get; set; }
        public int CodigoUEPSaida { get; set; }

        public required UnidadeExploracao UnidadeExploracaoOrigem { get; set; }
        public required UnidadeExploracao UnidadeExploracaoDestino { get; set; }

    }
}
