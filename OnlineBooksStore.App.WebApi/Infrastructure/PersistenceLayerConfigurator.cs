using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineBooksStore.Domain.Contracts.Repositories;
using OnlineBooksStore.Persistence.EF;

namespace OnlineBooksStore.App.WebApi.Infrastructure
{
    internal static class PersistenceLayerConfigurator
    {
        internal static IServiceCollection AddEntityFramework(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(configuration["ConnectionStrings:DefaultConnection"], b =>
                    b.MigrationsAssembly("OnlineBooksStore.App.WebApi"));
            });

            services.AddDistributedSqlServerCache(options =>
            {
                options.ConnectionString = configuration["ConnectionStrings:DefaultConnection"];
                options.SchemaName = "dbo";
                options.TableName = "SessionData";
            });

            return services;
        }

        internal static IServiceCollection AddEntityFrameworkIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IdentityDataContext>(options =>
                options.UseSqlServer(configuration["ConnectionStrings:IdentityConnection"], b =>
                    b.MigrationsAssembly("OnlineBooksStore.App.WebApi")));

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityDataContext>()
                .AddDefaultTokenProviders();

            return services;
        }

        internal static IServiceCollection AddSeession(this IServiceCollection services)
        {
            services.AddSession(options =>
            {
                options.Cookie.Name = "BooksStore.Session";
                options.IdleTimeout = TimeSpan.FromHours(24);
                options.Cookie.HttpOnly = false;
            });

            return services;
        }

        internal static IServiceCollection AddAntiforgery(this IServiceCollection services)
        {
            services.AddAntiforgery(options => {
                options.HeaderName = "X-XSRF-TOKEN";
            });

            return services;
        }

        internal static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IBooksRepository, BooksRepository>();
            services.AddTransient<ICategoriesRepository, CategoriesRepository>();
            services.AddTransient<IPublishersRepository, PublishersRepository>();
            services.AddTransient<IOrdersRepository, OrdersRepository>();

            return services;
        }
    }
}
