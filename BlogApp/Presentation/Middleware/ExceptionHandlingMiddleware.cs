using System.Text.Json;
using Domain.Exeptions;

namespace Presentation.Middleware;

/// <summary>
/// Middleware for handling exceptions and generating appropriate HTTP responses.
/// </summary>

public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger = logger;

    /// <summary>
    /// Invokes the middleware to handle exceptions and generate appropriate HTTP responses.
    /// </summary>
  
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (NotFound ex)
        {
            _logger.LogError(ex, "Resource not found");
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            context.Response.ContentType = "application/json";

            var errorResponse = new
            {
                ex.Message
            };

            var jsonResponse = JsonSerializer.Serialize(errorResponse);
            await context.Response.WriteAsync(jsonResponse);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError(ex, "Invalid operation");
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Response.ContentType = "application/json";

            var errorResponse = new
            {
                ex.Message
            };

            var jsonResponse = JsonSerializer.Serialize(errorResponse);
            await context.Response.WriteAsync(jsonResponse);
        }
        catch (BadHttpRequestException ex)
        {
            _logger.LogError(ex, "Failed to read parameter from the request body as JSON");
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Response.ContentType = "application/json";

            var errorResponse = new
            {
                Message = "Invalid request data.",
                Details = ex.Message
            };

            var jsonResponse = JsonSerializer.Serialize(errorResponse);
            await context.Response.WriteAsync(jsonResponse);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred");
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";

            var errorResponse = new
            {
                Message = "An unexpected error occurred."
            };

            var jsonResponse = JsonSerializer.Serialize(errorResponse);
            await context.Response.WriteAsync(jsonResponse);
        }
    }
}
