

using System.Diagnostics;
using Web.API.Common.Errors;
using Web.API.Common.Validations;

namespace Web.API.Middlewares
{
    public class HeaderValidationMiddleware : IMiddleware
    {

        private readonly IProblemDetails _problemDetails;

        public HeaderValidationMiddleware(IProblemDetails problemDetails)
        {
            _problemDetails = problemDetails;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var errors = new List<string>();
            if (context.Request.Headers.TryGetValue("traceId", out var traceIdFromHeader))
            {
                var activity = Activity.Current ?? new Activity("IncomingRequest");
                activity.SetParentId(traceIdFromHeader);
                activity.Start();

                context.TraceIdentifier = traceIdFromHeader;
            }
            foreach (var header in HeaderValidationRules.RequiredHeaders)
            {
                if (!context.Request.Headers.TryGetValue(header.Key, out var headerValue) || !header.Value(headerValue))
                {

                    errors.Add($"Invalid or missing '{header.Key}' header.");
                }
            }

            if (errors.Count > 0)
            {
                var problemDetails = _problemDetails.CreateProblemDetails(
                    context,
                    statusCode: 400,
                    title: "Header Validation Failed",
                    detail: "One or more headers are invalid or missing.",
                    instance: context.Request.Path,
                    type: "validation error"
                  
                );

                problemDetails.Extensions["errors"] = errors;

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = 400;
                await context.Response.WriteAsJsonAsync(problemDetails);
                return;
            }

            try
            {

            await next(context);
            }
            finally
            {
                Activity.Current?.Stop();
            }
        }
    }
}
