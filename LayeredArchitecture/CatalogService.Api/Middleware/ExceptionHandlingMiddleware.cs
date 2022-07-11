using System.Net.Mime;
using CategoryService.Application.Exceptions;

namespace CatalogService.Api.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleException(httpContext, ex);
        }
    }

    private static async Task HandleException(HttpContext context, Exception exception)
    {
        context.Response.ContentType = MediaTypeNames.Text.Plain;

        switch (exception)
        {
            case NotExistException notExistException:
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                await context.Response.WriteAsync($"Not found: {notExistException.Message}");
                break;
            case DuplicateException duplicateException:
                context.Response.StatusCode = StatusCodes.Status409Conflict;
                await context.Response.WriteAsync($"Duplicate: {duplicateException.Message}");
                break;
            case ValidationException validationException:
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync($"Not valid: {validationException.Message}");
                break;
            case { } ex:
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync($"ERROR: {ex.Message}");
                break;
        }
    }
}