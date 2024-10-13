﻿using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ShorteningService.Presentation.ExceptionHandlers
{
    public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            logger.LogError(exception, "Unhandled exception occurred. Occurence Time: {time}", DateTime.Now);


            var problemDetails = new ProblemDetails()
            {
                Status = StatusCodes.Status500InternalServerError,
                Detail = exception.Message,
                Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1"
            };

            problemDetails.Extensions.Add("traceId", httpContext.TraceIdentifier);

            httpContext.Response.StatusCode = problemDetails.Status.Value;

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
            return true;
        }
    }
}
