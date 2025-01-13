
using System.Net;
using Web.API.Common.Errors;

namespace Web.API.Middlewares
{
    public class GlobalExceptionHandlerMiddleware : IMiddleware
    {
        private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;
        private readonly IProblemDetails _problemDetails;

        public GlobalExceptionHandlerMiddleware(ILogger<GlobalExceptionHandlerMiddleware> logger, IProblemDetails problemDetails)
        {
            _logger = logger;
            _problemDetails = problemDetails;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                var statusCode = (int)HttpStatusCode.InternalServerError;
                var problemDetails = _problemDetails.CreateProblemDetails(
                context, 
                statusCode,
                title: "An error occurred",
                type: "server error",
                detail: "server error"
                 );

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = statusCode;

                await context.Response.WriteAsJsonAsync(problemDetails);
            }
        }
    }
}
