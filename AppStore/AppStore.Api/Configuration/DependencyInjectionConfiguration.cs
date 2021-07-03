using AppStore.Api.Mapper;
using AppStore.Domain.Interfaces;
using AppStore.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace AppStore.Api.Configuration
{
    public static class DependencyInjectionConfiguration
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IApplicationService, ApplicationService>();
            services.AddScoped<IApplicationMapper, ApplicationMapper>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserMapper, UserMapper>();
            services.ConfigureRepositoryServices(configuration);
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerOptionsConfig>();

            return services;
        }
    }
}