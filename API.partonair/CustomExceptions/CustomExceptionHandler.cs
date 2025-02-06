using DomainLayer.partonair.Exceptions;
using DomainLayer.partonair.Exceptions.Enums;

using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;

using System.Diagnostics;

namespace API.partonair.CustomExceptions
{
    public class CustomExceptionHandler(IProblemDetailsService problemDetailsService) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            httpContext.Response.StatusCode = exception switch
            {
                InfrastructureLayerException ex => ex.ErrorType switch
                {
                    InfrastructureLayerErrorType.ResourceNotFound => 404,
                    InfrastructureLayerErrorType.DatabaseConnectionError => 503,
                    InfrastructureLayerErrorType.ConcurrencyDatabaseException => 409,
                    InfrastructureLayerErrorType.CancelationDatabaseException => 499, // ou 400
                    _ => 500           
                },
                ApplicationLayerException ex => ex.ErrorType switch
                { 
                    ApplicationLayerErrorType.ConstraintViolationError => 409,
                    _ => 500
                },
                _ => StatusCodes.Status500InternalServerError
            };

            Activity? activity = httpContext.Features.Get<IHttpActivityFeature>()?.Activity;

            return await problemDetailsService.TryWriteAsync(new ProblemDetailsContext
            {
                HttpContext = httpContext,
                Exception = exception,
                ProblemDetails = new ProblemDetails
                {
                    Type = $"https://httpstatuses.io/{httpContext.Response.StatusCode}",
                    Title = httpContext.Response.StatusCode switch
                    {
                        404 => "Resource not found",
                        503 => "Database unavailable",
                        409 => "Concurrency conflict",
                        499 => "Request canceled",
                        _ => "Internal error"
                    },
                    Detail = exception.Message,
                    Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}",
                    Extensions = new Dictionary<string, object?>
                    {
                        { "requestId", httpContext.TraceIdentifier },
                        { "traceId", activity?.Id }
                    }
                }
            });
        }
        
    }

}
