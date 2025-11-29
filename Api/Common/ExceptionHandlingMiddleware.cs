using System.Text.Json;
using Api.Errors;
using Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Api.Common;

public class ExceptionHandlingMiddleware(
    RequestDelegate next,
    ILogger<ExceptionHandlingMiddleware> logger,
    IHostEnvironment env)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);

            // Handle 404 for routes that don't exist
            if (context.Response.StatusCode == 404 && !context.Response.HasStarted)
            {
                await Handle404Async(context);
            }
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        if (context.Response.HasStarted)
        {
            logger.LogWarning("Cannot handle exception, response has already started");
            return;
        }

        var (statusCode, message, errors) = exception switch
        {
            ValidationException ve => (400, ve.Message, ve.Errors),
            NotFoundException nfe => (404, nfe.Message, null),
            BadRequestException bre => (400, bre.Message, null),
            ConflictException ce => (409, ce.Message, null),
            UnauthorizedException ue => (401, ue.Message, null),
            ForbiddenException fe => (403, fe.Message, null),
            DbUpdateException dbEx => HandleDbUpdateException(dbEx),
            _ => (500, 
                env.IsDevelopment() ? exception.Message : "An error occurred", 
                env.IsDevelopment() ? new List<string> { exception.StackTrace ?? "" } : null)
        };

        logger.LogError(exception, "Error occurred: {Message}", exception.Message);

        var response = new ErrorResponse(statusCode, message, errors);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(response, options));
    }

    private (int statusCode, string message, List<string>? errors) HandleDbUpdateException(DbUpdateException dbEx)
    {
        var innerException = dbEx.InnerException?.Message ?? dbEx.Message;

        // Check for unique constraint violation
        if (innerException.Contains("duplicate key", StringComparison.OrdinalIgnoreCase) ||
            innerException.Contains("UNIQUE constraint", StringComparison.OrdinalIgnoreCase) ||
            innerException.Contains("cannot insert duplicate", StringComparison.OrdinalIgnoreCase))
        {
            return (409, "A record with this value already exists", null);
        }

        // Check for foreign key constraint violation
        if (innerException.Contains("FOREIGN KEY constraint", StringComparison.OrdinalIgnoreCase) ||
            innerException.Contains("conflicted with the FOREIGN KEY", StringComparison.OrdinalIgnoreCase))
        {
            return (400, "Invalid reference: The related record does not exist", null);
        }

        // Check for required field violation
        if (innerException.Contains("Cannot insert NULL", StringComparison.OrdinalIgnoreCase) ||
            innerException.Contains("NOT NULL constraint", StringComparison.OrdinalIgnoreCase))
        {
            return (400, "Required field is missing", null);
        }

        // Generic database error
        return (500,
            env.IsDevelopment() ? innerException : "A database error occurred",
            env.IsDevelopment() ? new List<string> { dbEx.StackTrace ?? "" } : null);
    }

    private async Task Handle404Async(HttpContext context)
    {
        var response = new ErrorResponse(
            statusCode: 404,
            message: $"Endpoint '{context.Request.Path}' not found"
        );

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = 404;

        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(response, options));
    }
}