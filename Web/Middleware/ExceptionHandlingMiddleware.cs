using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Web.Middleware
{
    internal sealed class ExceptionHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger) => _logger = logger;

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);

                await HandleExceptionAsync(context, e);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            context.Response.StatusCode = exception switch
            {
                BadHttpRequestException or ValidationException => StatusCodes.Status400BadRequest,
                EntryPointNotFoundException => StatusCodes.Status404NotFound,
                _ => StatusCodes.Status500InternalServerError
            };

            return context.Response.CompleteAsync();
        }
    }
}
