using Elmah.Io.AspNetCore;
using Elmah.Io.Extensions.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace AppStore.Api.Configuration
{
    public static class LoggerConfig
    {
        public static IServiceCollection AddLoggingConfiguration(this IServiceCollection services)
        {
            services.AddElmahIo(o =>
            {
                o.ApiKey = "b3063cc733534c7f90bfe603abc6445c";
                o.LogId = new Guid("1d339ba5-8227-46e7-a834-8c91394f017a");
            });

            services.AddLogging(builder => 
            { 
                builder.AddElmahIo(o =>
                {
                    o.ApiKey = "b3063cc733534c7f90bfe603abc6445c";
                    o.LogId = new Guid("1d339ba5-8227-46e7-a834-8c91394f017a");
                });

                builder.AddFilter<ElmahIoLoggerProvider>(null, LogLevel.Warning);
            });

            return services;
        }

        public static IApplicationBuilder UseLoggingConfiguration(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseElmahIo();

            return applicationBuilder;
        }
    }
}