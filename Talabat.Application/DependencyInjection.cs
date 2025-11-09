using Microsoft.Extensions.DependencyInjection;
using Talabat.Application.Maping;
using Talabat.Application.Services;
using Talabat.Application.Services.Order;
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

            services.AddScoped(typeof(IOrderService), typeof(OrderService));
            services.AddScoped(typeof(Func<IOrderService>), sp =>
            {
              return () => sp.GetRequiredService<IOrderService>();
            });

            return services;
        }
    }
}
