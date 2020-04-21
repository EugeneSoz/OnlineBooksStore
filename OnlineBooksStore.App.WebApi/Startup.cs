﻿using System;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineBooksStore.App.WebApi.Models.Database;
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

            //services.AddSpaStaticFiles(configuration =>
            //{
            //    configuration.RootPath = "ClientApp/dist/ClientApp";
            //});

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
            //app.UseSpaStaticFiles();
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
