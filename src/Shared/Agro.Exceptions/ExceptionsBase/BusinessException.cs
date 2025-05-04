using System.Net;

namespace Agro.Exceptions.ExceptionsBase
{
    public class BusinessException(string message) : AgroException(message)
    {
        public override IList<string> GetErrorMessages() => [Message];

        public override HttpStatusCode GetStatusCode() => HttpStatusCode.BadRequest;
    }
}
