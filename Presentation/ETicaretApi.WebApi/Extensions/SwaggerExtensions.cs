using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace ETicaretApi.WebApi.Extensions
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "ETicaret API",
                    Version = "v1"
                });
            });

            return services;
        }
    }
}
