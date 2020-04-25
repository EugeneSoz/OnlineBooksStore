using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineBooksStore.App.WebApi.Models.Database;
using OnlineBooksStore.Domain.Contracts.Services;
using OnlineBooksStore.Domain.Services;

namespace OnlineBooksStore.App.WebApi.Infrastructure
{
    internal static class AppConfigurator
    {
        internal static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IDbDataService, DbDataService>();
            services.AddTransient<MigrationsManager>();

            return services;
        }

        internal static IApplicationBuilder UseSpa(this IApplicationBuilder application, IConfiguration configuration)
        {
            application.UseSpa(spa =>
            {
                var strategy = configuration.GetValue<string>("DevTools:ConnectionStrategy");
                if (strategy == "proxy")
                {
                    spa.UseProxyToSpaDevelopmentServer("http://localhost:4200/");
                }
                else if (strategy == "managed")
                {
                    spa.Options.SourcePath = "../angular-client";
                    spa.UseAngularCliServer("start");
                }
            });

            return application;
        }
    }
}
