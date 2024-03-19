using Weatherman.Core.Exceptions;

namespace Weatherman.Api.Middlewares
{
    public class ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
    {
        private readonly RequestDelegate _next = next;

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (BaseException ex)
            {
                await HandleKnownExceptionAsync(context, ex);
            }
            catch (Exception ex)
            {
                await HandleUnknownExceptionAsync(context, ex);
            }
        }

        private async Task HandleKnownExceptionAsync(HttpContext context, Exception exception)
        {
            logger.LogInformation(exception, "Custom Exception has been raised");
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsync(exception.Message);
        }

        private async Task HandleUnknownExceptionAsync(HttpContext context, Exception exception)
        {
            logger.LogError(exception, "An unhandled exception occurred");
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsync(exception.Message);
        }
    }
}
