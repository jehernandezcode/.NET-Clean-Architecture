
using Web.API.Common.Errors;

namespace Web.API.Middlewares
{
    public class GlobalExceptionHandlerMiddleware : IMiddleware
    {
        private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

        public GlobalExceptionHandlerMiddleware(ILogger<GlobalExceptionHandlerMiddleware> logger)
        {
            _logger = logger;
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
                var dataError = ErrorDetails.list["InternalError"];
                context.Response.StatusCode = (int)dataError.StatusCode;

                var errorResponse = new ErrorResponseHttpBuilder(dataError.StatusCode)
                    .WithTitle(dataError.Title)
                    .WithType(dataError.Type)
                    .WithDetail(dataError.Detail);

                await errorResponse.WriteAsync(context);
            }
        }
    }
}
