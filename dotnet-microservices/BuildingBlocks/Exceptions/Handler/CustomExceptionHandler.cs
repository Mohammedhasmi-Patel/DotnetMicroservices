using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuildingBlocks.Exceptions.Handler;

public class CustomExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
    {
        //throw new NotImplementedException();
        (string Detail, string Title, int StatusCode) details = exception switch
        {
            NotFoundException =>
            (
                exception.Message,
                exception.GetType().Name,
                context.Response.StatusCode = 404
            ),
            BadRequestException =>
            (
                exception.Message,
                exception.GetType().Name,
                context.Response.StatusCode = 400
            ),
            ConflictException =>
            (
                exception.Message,
                exception.GetType().Name,
                context.Response.StatusCode = 409
            ),
            ValidationCustomException =>
            (
                exception.Message,
                exception.GetType().Name,
                context.Response.StatusCode = 422
            ),
            _ =>
            (
                exception.Message,
                exception.GetType().Name,
                context.Response.StatusCode = 500
            )
        };

        var problemDetails = new ProblemDetails
        {
            Title = details.Title,
            Detail = details.Detail,
            Status = details.StatusCode,
            Instance = context.Request.Path
        };

        problemDetails.Extensions.Add("traceId", context.TraceIdentifier);
        await context.Response.WriteAsJsonAsync( problemDetails, cancellationToken );
        return true;
    }
}
