using System.Net;

namespace Agro.Exceptions.ExceptionsBase
{
    public abstract class AgroException(string message) : SystemException(message)
    {
        public abstract IList<string> GetErrorMessages();
        public abstract HttpStatusCode GetStatusCode();
    }
}
