namespace Talabat.Infrastructure.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<StoreDbContext>(options =>
                         options.UseLazyLoadingProxies()
                                .UseSqlServer(configuration.GetConnectionString("StoreConnection")));

            services.AddScoped<IStoreDbIntializer, StoreDbInitializer>();
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork.UnitOfWork));
           

            services.AddDbContext<StoreIdentityDbConetxt>(options =>
                         options.UseLazyLoadingProxies()
                                .UseSqlServer(configuration.GetConnectionString("IdentityConnection")));

           

            services.AddScoped(typeof(IStoreIdentityInializer), typeof(StoreIdentityDbInitializer));
           
            return services;

        }
    }
}
