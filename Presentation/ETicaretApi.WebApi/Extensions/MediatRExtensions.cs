using ETicaretApi.Application.Features.MediatorDesignPattern.Handlers.OrderHandlers;
using Microsoft.Extensions.DependencyInjection;

namespace ETicaretApi.WebApi.Extensions
{
    public static class MediatRExtensions
    {
        public static IServiceCollection AddMediatorServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(getOrderQueryHandler).Assembly));

            return services;
        }
    }
}
