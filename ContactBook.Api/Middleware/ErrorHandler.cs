using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using ContactBook.Application.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace ContactBook.Api.Middleware;

public class ErrorHandler
{
    private readonly RequestDelegate _next;

    public ErrorHandler(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = HttpStatusCode.InternalServerError;       
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;

        if (exception is ValidationException exceptionVal)
        {
            var result = JsonSerializer.Serialize(new { error = exceptionVal.Errors });
            return context.Response.WriteAsync(result);
        } else {
            var message = exception.Message;
            var result = JsonSerializer.Serialize(new { error = new string[] { message } });
            return context.Response.WriteAsync(result);
        }

    }
}