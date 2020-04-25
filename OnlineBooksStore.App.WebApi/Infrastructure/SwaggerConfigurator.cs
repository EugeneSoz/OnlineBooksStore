using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace OnlineBooksStore.App.WebApi.Infrastructure
{
    internal static class SwaggerConfigurator
    {
        internal static IServiceCollection AddSwagger(this IServiceCollection services, IHostingEnvironment environment)
        {
            services.AddSwaggerGen(options =>
                options.SwaggerDoc("v1", new Info
                {
                    Title = "OnlineBooksStore Web API service", 
                    Version = "v1",
                    Description = $"Environment: {environment.EnvironmentName}"
                }));

            return services;
        }

        internal static IApplicationBuilder UseSwaggerWithUi(this IApplicationBuilder application)
        {
            application.UseSwagger();

            application.UseSwaggerUI(options =>
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "OnlineBooksStore API v1"));

            return application;
        }
    }
}
