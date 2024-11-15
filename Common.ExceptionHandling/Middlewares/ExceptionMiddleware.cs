using System.Text;
using System.Text.Json;
using Common.Domain.Api;
using Common.ExceptionHandling.Exceptions.ApiExceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Common.ExceptionHandling.Middlewares;

public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
{
    public const string DefaultErrorMessage = "An unexpected error occurred. Please contact support at support@nokok.co";
    
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await next(httpContext);
        }
        catch (ApiException ex)
        {
            var dateNow = $"{DateTime.UtcNow:HH:mm:ss zzz}";
            logger.LogWarning("An Api exception occured with status code {statusCode} @ {dateNow}: {message}", ex.StatusCode, dateNow, ex.Message);
            var errorResponse = new ApiResult<object>
            {
                StatusCode = ex.StatusCode,
                Message = ex.Message,
                Succeeded = false,
                Data = null
            };
            await httpContext.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
        }
        catch (Exception ex)
        {
            var dateNow = $"{DateTime.UtcNow:HH:mm:ss zzz}";
            logger.LogError(ex, "An exception has occured @ {dateNow}: {message}", dateNow, ex.Message);
            var errorResponse = new ApiResult<object>
            {
                StatusCode = 500,
                Message = DefaultErrorMessage,
                Succeeded = false,
                Data = null
            };
            await httpContext.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
        }
    }
}