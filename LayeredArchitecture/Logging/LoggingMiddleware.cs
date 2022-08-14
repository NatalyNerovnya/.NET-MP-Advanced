using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;

namespace Logging;

public class LoggingMiddleware
{
    private const string CorrelationIdHeaderKey = "X-Correlation-ID";
    private readonly RequestDelegate _next;
    private readonly ILogger<LoggingMiddleware> _logger;

    public LoggingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
        _logger = loggerFactory.CreateLogger<LoggingMiddleware>();
    }

    public async Task Invoke(HttpContext httpContext)
    {
        string correlationId;
        if (httpContext.Request.Headers.TryGetValue(CorrelationIdHeaderKey, out StringValues correlationIds))
        {
            correlationId = correlationIds.FirstOrDefault(k => k.Equals(CorrelationIdHeaderKey));
            _logger.LogInformation($"CorrelationId {correlationId} is from header");
        }
        else
        {
            correlationId = Guid.NewGuid().ToString();
            httpContext.Request.Headers.Add(CorrelationIdHeaderKey, correlationId);
            _logger.LogInformation($"CorrelationId {correlationId} is newly generated");
        }

        httpContext.Response.OnStarting(() =>
        {
            if (!httpContext.Response.Headers.TryGetValue(CorrelationIdHeaderKey, out correlationIds))
            {
                httpContext.Response.Headers.Add(CorrelationIdHeaderKey, correlationId);
            }

            return Task.CompletedTask;
        });

        await _next.Invoke(httpContext);
    }
}