namespace Agro.Communication.Request
{
    public class RequestRegisterSaidaAnimaisJson
    {
        public DateTime DataSaida { get; set; }
        public int QuantidadeAnimais { get; set; }
        public int CodigoUEPOrigem { get; set; }
        public int CodigoUEPSaida { get; set; }
    }
}
