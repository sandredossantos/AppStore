using AppStore.Domain.Interfaces;
using AppStore.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace AppStore.Service
{
    public static class ServiceConfiguration
    {
        public static IServiceCollection ConfigureRepositoryServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RepositorySettings>(configuration.GetSection(nameof(RepositorySettings)));
            services.AddSingleton<IRepositorySettings>(r => r.GetRequiredService<IOptions<RepositorySettings>>().Value);
            services.AddScoped<IAplicationRepository, ApplicationRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPurchaseRepository, PurchaseRepository>();

            return services;
        }
    }
}