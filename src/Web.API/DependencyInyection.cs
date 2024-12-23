
using Web.API.Middlewares;

namespace Web.API
{
    public static class DependencyInyection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddTransient<GlobalExceptionHandlerMiddleware>();

            return services;
        }
    }
}
