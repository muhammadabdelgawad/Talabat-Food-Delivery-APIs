using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;


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

            services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));

            services.Configure<RedisSettings>(configuration.GetSection("RedisSettings"));
            return services;
        }
    }
}
