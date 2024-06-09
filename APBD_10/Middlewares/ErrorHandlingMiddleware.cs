using System.Net;
using APBD_10.Exceptions;

namespace APBD_10.Middlewares;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleException(context, ex);
        }
        
    }

    private Task HandleException(HttpContext context, Exception exception)
    {
        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        context.Response.ContentType = "application/json";
        var response = new
        {
            error = new
            {
                message = "An error occured while processing your request.",
                detail = exception.Message
            }
        };
        var jsonResponse = System.Text.Json.JsonSerializer.Serialize(response);
        return context.Response.WriteAsync(jsonResponse);
    }
    
}