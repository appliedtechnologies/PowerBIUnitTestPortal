
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.OData;

public class UnhandledExceptionFilterAttribute : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        var e = context.Exception;

        if(e.InnerException != null)
            context.Result = new ObjectResult(new ODataError { ErrorCode = "500", Message = e.Message, InnerError = new ODataInnerError(e.InnerException) }) { StatusCode = (int)HttpStatusCode.InternalServerError };
        else
            context.Result = new ObjectResult(new ODataError { ErrorCode = "500", Message = e.Message }) { StatusCode = (int)HttpStatusCode.InternalServerError };
    }
}
