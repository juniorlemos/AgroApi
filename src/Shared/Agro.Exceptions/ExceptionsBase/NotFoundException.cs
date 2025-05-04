using System.Net;

namespace Agro.Exceptions.ExceptionsBase
{
    public class NotFoundException(string message) : AgroException(message)
    {
        public override IList<string> GetErrorMessages() => [Message];

        public override HttpStatusCode GetStatusCode() => HttpStatusCode.NotFound;
    }
}
