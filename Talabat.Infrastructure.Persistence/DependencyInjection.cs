using Talabat.Application.Abstraction.Services;
using Talabat.Infrastructure.Persistence._Data.Interceptors;
using Talabat.Infrastructure.Persistence.Services;

namespace Talabat.Infrastructure.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<StoreDbContext>((serviceProvider,options) =>
                         options.UseLazyLoadingProxies()
                                .UseSqlServer(configuration.GetConnectionString("StoreConnection"))
                                .AddInterceptors(serviceProvider.GetRequiredService<AuditInterceptor>()));

            services.AddScoped<IStoreDbIntializer, StoreDbInitializer>();
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork.UnitOfWork));
           

            services.AddDbContext<StoreIdentityDbConetxt>(options =>
                         options.UseLazyLoadingProxies()
                                .UseSqlServer(configuration.GetConnectionString("IdentityConnection")));

           

            services.AddScoped(typeof(IStoreIdentityInializer), typeof(StoreIdentityDbInitializer));
            services.AddScoped<ILoggedInUserService, LoggedInUserService>();
            return services;

        }
    }
}
