using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using Talabat.Application.Abstraction.Services.Cache;
using Talabat.Infrastructure.Cache;
using Talabat.Infrastructure.Payment_Service;
namespace Talabat.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this
            IServiceCollection services, IConfiguration configuration )
        {
            services.AddSingleton(typeof(IConnectionMultiplexer), (serviceProvider) =>
            {
                var connectionString = configuration.GetConnectionString("Redis");
                var connectionMuiltiplexerObj= ConnectionMultiplexer.Connect(connectionString!);
                return connectionMuiltiplexerObj;
            });

            services.AddSingleton(typeof(ICacheService), typeof(CacheService));
           
            services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));
            
            services.AddScoped(typeof(IPaymentService), typeof(PaymentService));

            services.Configure<RedisSettings>(configuration.GetSection("RedisSettings"));
           
            services.Configure<StripeSettings>(configuration.GetSection("StripeSettings"));


            return services;
        }
    }
}
