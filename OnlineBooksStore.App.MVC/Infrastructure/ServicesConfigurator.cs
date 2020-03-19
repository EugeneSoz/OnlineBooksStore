using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineBooksStore.App.Handlers.Command;
using OnlineBooksStore.App.Handlers.Mapping;
using OnlineBooksStore.App.Handlers.Query;
using OnlineBooksStore.Domain.Contracts.Repositories;
using OnlineBooksStore.Persistence.EF;
using AutoMapper;

namespace OnlineBooksStore.App.MVC.Infrastructure
{
    internal static class ServicesConfigurator
    {
        internal static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IBooksRepository, BooksRepository>();
            services.AddTransient<ICategoriesRepository, CategoriesRepository>();
            services.AddTransient<IPublishersRepository, PublishersRepository>();
            services.AddTransient<IOrdersRepository, OrdersRepository>();
            services.AddTransient<IWebServiceRepository, WebServiceRepository>();

            var configString = configuration["ConnectionStrings:DefaultConnection"];
            services.AddDbContext<DataContext>(options => options.UseSqlServer(configString, b => b.MigrationsAssembly("OnlineBooksStore.App.MVC")));

            return services;
        }
        
        internal static IServiceCollection AddCacheAndSession(this IServiceCollection services, IConfiguration configuration)
        {
            var configString = configuration["ConnectionStrings:DefaultConnection"];
            services.AddDistributedSqlServerCache(options =>
            {
                options.ConnectionString = configString;
                options.SchemaName = "dbo";
                options.TableName = "SessionData";
            });
            services.AddSession(options =>
            {
                options.Cookie.Name = "SportsStore.Session";
                options.IdleTimeout = TimeSpan.FromHours(48);
                options.Cookie.HttpOnly = false;
            });

            return services;
        }

        internal static IServiceCollection AddCommandAndQuery(this IServiceCollection services)
        {
            services.AddTransient<PublisherCommandHandler>();
            services.AddTransient<PublisherQueryHandler>();

            return services;
        }

        internal static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            return services.AddAutoMapper(typeof(ApplicationLayerMappings));
        }
    }
}