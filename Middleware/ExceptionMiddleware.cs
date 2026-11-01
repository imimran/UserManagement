using System.Net;
using System.Text.Json;
using UserManagement.Api.Common.Exceptions;
using UserManagement.Api.Common.Responses;

namespace UserManagement.Api.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    private readonly ILogger<ExceptionMiddleware> _logger;


    public ExceptionMiddleware(
        RequestDelegate next,
        ILogger<ExceptionMiddleware> logger)
    {
        _next = next;

        _logger = logger;
    }


    public async Task InvokeAsync(
        HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            _logger.LogError(
                exception,
                "Unhandled exception.");

            await HandleExceptionAsync(
                context,
                exception);
        }
    }


    private static async Task HandleExceptionAsync(
        HttpContext context,
        Exception exception)
    {
        var statusCode =
            exception is AppException appException
                ? appException.StatusCode
                : (int)HttpStatusCode.InternalServerError;


        var message =
            exception is AppException
                ? exception.Message
                : "An unexpected error occurred.";


        object? errors =
            exception is AppException app
                ? app.Errors
                : null;


        context.Response.StatusCode =
            statusCode;

        context.Response.ContentType =
            "application/json";


        var response =
            ApiResponse<object>.Fail(
                message,
                errors);


        var json =
            JsonSerializer.Serialize(response);


        await context.Response.WriteAsync(json);
    }
}