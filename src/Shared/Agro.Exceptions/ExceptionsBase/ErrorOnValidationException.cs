using System.Net;

namespace Agro.Exceptions.ExceptionsBase
{
    public class ErrorOnValidationException(IList<string> errorMessages) : AgroException(string.Empty)
    {
        private readonly IList<string> _errorMessages = errorMessages;

        public override IList<string> GetErrorMessages() => _errorMessages;

        public override HttpStatusCode GetStatusCode() => HttpStatusCode.BadRequest;
    }
}
