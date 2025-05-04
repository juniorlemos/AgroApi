using Agro.Communication.Response;
using Agro.Exceptions;
using Agro.Exceptions.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Agro.API.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is AgroException agroException)
                HandleProjectException(agroException, context);
            else
                ThrowUnknowException(context);
        }

        private static void HandleProjectException(AgroException agroException, ExceptionContext context)
        {
            context.HttpContext.Response.StatusCode = (int)agroException.GetStatusCode();
            context.Result = new ObjectResult(new ResponseErrorJson(agroException.GetErrorMessages()));
        }

        private static void ThrowUnknowException(ExceptionContext context)
        {
            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Result = new ObjectResult(new ResponseErrorJson(ResourceMessagesException.UNKNOWN_ERROR));
        }
    }
}