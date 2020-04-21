using System;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineBooksStore.App.Handlers.Command;
using OnlineBooksStore.App.Handlers.Query;
using OnlineBooksStore.App.WebApi.Models.Database;
using OnlineBooksStore.Domain.Contracts.Repositories;
using OnlineBooksStore.Domain.Contracts.Services;
using OnlineBooksStore.Domain.Services;
using OnlineBooksStore.Persistence.EF;
using Swashbuckle.AspNetCore.Swagger;

namespace OnlineBooksStore.App.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwaggerGen(options =>
                options.SwaggerDoc("v1", new Info { Title = "BooksStore API", Version = "v1" }));

            services.AddTransient<MigrationsManager>();

            services.AddDbContext<IdentityDataContext>(options =>
                options.UseSqlServer(Configuration["ConnectionStrings:IdentityConnection"], b =>
                    b.MigrationsAssembly("OnlineBooksStore.App.WebApi")));

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityDataContext>()
                .AddDefaultTokenProviders();

            services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"], b =>
                    b.MigrationsAssembly("OnlineBooksStore.App.WebApi"));
            });

            services.AddDistributedSqlServerCache(options =>
            {
                options.ConnectionString = Configuration["ConnectionStrings:DefaultConnection"];
                options.SchemaName = "dbo";
                options.TableName = "SessionData";
            });

            services.AddSession(options =>
            {
                options.Cookie.Name = "BooksStore.Session";
                options.IdleTimeout = TimeSpan.FromHours(24);
                options.Cookie.HttpOnly = false;
            });

            services.AddAntiforgery(options => {
                options.HeaderName = "X-XSRF-TOKEN";
            });

            services.AddTransient<IBooksRepository, BooksRepository>();
            services.AddTransient<ICategoriesRepository, CategoriesRepository>();
            services.AddTransient<IPublishersRepository, PublishersRepository>();
            services.AddTransient<IOrdersRepository, OrdersRepository>();

            services.AddTransient<BookQueryHandler>();
            services.AddTransient<CategoryQueryHandler>();
            services.AddTransient<PublisherQueryHandler>();
            services.AddTransient<OrderQueryHandler>();

            services.AddTransient<BookCommandHandler>();
            services.AddTransient<CategoryCommandHandler>();
            services.AddTransient<PublisherCommandHandler>();
            services.AddTransient<OrderCommandHandler>();
            services.AddTransient<TablesCommandHandlers>();
            services.AddTransient<IDbDataService, DbDataService>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, 
            IdentityDataContext identityContext,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IAntiforgery antiforgery)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseAuthentication();

            app.Use(nextDelegate => requestContext => {
                if (requestContext.Request.Path.StartsWithSegments("/api")
                        || requestContext.Request.Path.StartsWithSegments("/"))
                {
                    requestContext.Response.Cookies.Append("XSRF-TOKEN",
                    antiforgery.GetAndStoreTokens(requestContext).RequestToken);
                }
                return nextDelegate(requestContext);
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseSwagger();

            app.UseSwaggerUI(options =>
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "OnlineBooksStore API v1"));

            app.UseSpa(spa =>
            {
                var strategy = Configuration.GetValue<string>("DevTools:ConnectionStrategy");
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

            IdentitySeedData.SeedDatabase(identityContext, userManager, roleManager).GetAwaiter().GetResult();
        }
    }
}
