

using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace Web.API.Common.Errors
    {
        public class ErrorResponseHttpBuilder
        {
            private readonly ProblemDetails _problemDetails;

        public ErrorResponseHttpBuilder(HttpStatusCode statusCode )
            {
                _problemDetails = new ProblemDetails
                {
                    Status = (int)statusCode,
                    Type = "Server Error",
                    Title = "Server Error",
                    Detail = "An Internal error"
                };
            }

        public ErrorResponseHttpBuilder WithTitle(string title)
            {
                _problemDetails.Title = title;
                return this;
            }

            public ErrorResponseHttpBuilder WithDetail(string detail)
            {
                _problemDetails.Detail = detail;
                return this;
            }

            public ErrorResponseHttpBuilder WithType(string type)
            {
                _problemDetails.Type = type;
                return this;
            }

            public ErrorResponseHttpBuilder WithInstance(string instance)
            {
                _problemDetails.Instance = instance;
                return this;
            }

            public ErrorResponseHttpBuilder WithCustomExtension(string key, object value)
            {
                _problemDetails.Extensions[key] = value;
                return this;
            }

            public async Task WriteAsync(HttpContext context, Exception? exception = null)
            {
                if (exception != null)
                {
                    //logger.LogError(exception, exception.Message);
                }

                context.Response.StatusCode = _problemDetails.Status ?? (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                string json = JsonSerializer.Serialize(_problemDetails);
                await context.Response.WriteAsync(json);
            }
        }
    }

