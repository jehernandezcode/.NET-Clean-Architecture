

using Web.API.Common.Errors;
using Web.API.Common.Validations;

namespace Web.API.Middlewares
{
    public class HeaderValidationMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var errors = new List<string>();
            foreach (var header in HeaderValidationRules.RequiredHeaders)
            {
                if (!context.Request.Headers.TryGetValue(header.Key, out var headerValue) || !header.Value(headerValue))
                {

                    errors.Add($"Invalid or missing '{header.Key}' header.");
                }
            }

            if (errors.Count > 0)
            {
                var dataError = ErrorDetails.list["HeaderError"];
                var errorResponse = new ErrorResponseHttpBuilder(dataError.StatusCode)
                    .WithTitle(dataError.Title)
                    .WithType(dataError.Type)
                    .WithDetail(dataError.Detail)
                    .WithCustomExtension(dataError.ExtensionName, String.Join(" ", errors));

                await errorResponse.WriteAsync(context);
                return;
            }

            await next(context);
        }
    }
}
