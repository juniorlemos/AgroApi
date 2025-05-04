namespace Agro.Communication.Response
{
    public class ResponseSaidaAnimaisJson
    {
        public int Id { get; set; }
        public DateTime DataSaida { get; private set; } = DateTime.UtcNow;
        public int QuantidadeAnimais { get; set; }
        public int CodigoUEPOrigem { get; set; }
        public int CodigoUEPSaida { get; set; }
    }
}
