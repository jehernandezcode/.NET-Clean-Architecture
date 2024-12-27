
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

            return services;
        }
    }
}
