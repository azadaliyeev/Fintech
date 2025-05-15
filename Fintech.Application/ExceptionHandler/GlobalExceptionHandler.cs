using System.Net;
using Fintech.Shared.ServiceResults;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace Fintech.Application.ExceptionHandler;

public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
        CancellationToken cancellationToken)
    {
        var errorAsDto = ServiceResult.Fail(exception.Message);

        httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        httpContext.Response.ContentType = "application/json";
        await httpContext.Response.WriteAsJsonAsync(errorAsDto, cancellationToken: cancellationToken);

        return true;
    }
}