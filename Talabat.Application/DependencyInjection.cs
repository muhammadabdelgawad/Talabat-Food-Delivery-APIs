using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Talabat.Application.Abstraction.Services;
using Talabat.Application.Abstraction.Services.Basket;
using Talabat.Application.Maping;
using Talabat.Application.Services;
using Talabat.Domain.Contracts.Infrastructure;
using Talabat.Infrastructure.Persistence.Repositories.Baskets;
namespace Talabat.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            
            services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>());
            services.AddScoped(typeof(IServiceManager), typeof(ServiceManager));
            services.AddScoped<ProductPictureUrlResolver>();

            services.AddScoped(typeof(IBasketService), typeof(BasketService));
            services.AddScoped(typeof(Func<IBasketService>), sp =>
{
    return () => sp.GetRequiredService<IBasketService>();
});
                
                return services;
        }
    }
}
