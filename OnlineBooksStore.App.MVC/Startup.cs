using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using OnlineBooksStore.Domain.Contracts.Repositories;
using OnlineBooksStore.Persistence.EF;

namespace OnlineBooksStore.App.MVC
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddJsonOptions(options => options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Serialize);
            services.AddTransient<IBooksRepository, BooksRepository>();
            services.AddTransient<ICategoriesRepository, CategoriesRepository>();
            services.AddTransient<IPublishersRepository, PublishersRepository>();
            services.AddTransient<IOrdersRepository, OrdersRepository>();
            services.AddTransient<IWebServiceRepository, WebServiceRepository>();

            var configString = Configuration["ConnectionStrings:DefaultConnection"];
            services.AddDbContext<DataContext>(options => options.UseSqlServer(configString, b => b.MigrationsAssembly("OnlineBooksStore.App.MVC")));

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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseSession();
            app.UseMvcWithDefaultRoute();
        }
    }
}
