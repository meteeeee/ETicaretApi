using ETicaretApi.Application.Features.CQRSDesignPattern.Handlers.CategoryHandlers;
using ETicaretApi.Application.Features.CQRSDesignPattern.Handlers.ProductHandlers;
using ETicaretApi.Application.Features.CQRSDesignPattern.Handlers.UserRegisterHandlers;
using Microsoft.Extensions.DependencyInjection;

namespace ETicaretApi.WebApi.Extensions
{
    public static class ServiceRegistrationExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Category
            services.AddScoped<getCategoryQueryHandler>();
            services.AddScoped<getCategoryByIdQueryHandler>();
            services.AddScoped<CreateCategoryCommandHandler>();
            services.AddScoped<RemoveCategoryCommandHandler>();
            services.AddScoped<UpdateCategoryCommandHandler>();

            // Product
            services.AddScoped<getProductQueryHandler>();
            services.AddScoped<getProductByIdQueryHandler>();
            services.AddScoped<getProductWithCategoryQueryHandler>();
            services.AddScoped<CreateProductCommandHandler>();
            services.AddScoped<RemoveProductCommandHandler>();
            services.AddScoped<UpdateProductCommandHandler>();

            // User
            services.AddScoped<CreateUserRegisterCommandHandler>();

            return services;
        }
    }
}
