using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Web.API.Common.Validations;

namespace Web.API.Common.Swagger
{
    public class GlobalHeaderDocumentFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
            {
                operation.Parameters = new List<OpenApiParameter>();
            }

            foreach (var header in HeaderValidationRules.RequiredHeaders)
            {
                operation.Parameters.Add(new OpenApiParameter
                {
                    Name = header.Key,
                    In = ParameterLocation.Header,
                    Required = true,
                    Schema = new OpenApiSchema
                    {
                        Type = "string",
                        Description = $"Validation logic: {header.Key}"
                    }
                });
            }
        }
    }
}
