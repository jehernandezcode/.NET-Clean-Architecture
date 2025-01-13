
using Microsoft.AspNetCore.Mvc;
using Web.API.Common.Errors;
using Web.API.Common.Swagger;
using Web.API.Middlewares;

namespace Web.API
{
    public static class DependencyInyection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.OperationFilter<GlobalHeaderDocumentFilter>();
            });
            services.AddTransient<HeaderValidationMiddleware>();
            services.AddTransient<GlobalExceptionHandlerMiddleware>();

            services.AddSingleton(resolver =>
            {
                var options = new ApiBehaviorOptions();
                resolver.GetRequiredService<IConfiguration>().Bind("ApiBehaviorOptions", options);
                return options;
            });


            services.AddSingleton<IProblemDetails, EasyPosProblemDetailsFactory>();

            return services;
        }
    }
}
