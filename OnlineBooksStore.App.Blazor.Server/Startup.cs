using System;
using System.Net.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OnlineBooksStore.Domain.Contracts.Services;
using OnlineBooksStore.Domain.Services;
using OnlineBooksStore.Integration.Client.Admin;
using OnlineBooksStore.Integration.Client.Store;
using OnlineBooksStore.Integration.Contracts.Admin;
using OnlineBooksStore.Integration.Contracts.Store;

namespace OnlineBooksStore.App.Blazor.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();

            services.AddHttpClient("default", client => client.BaseAddress = new Uri("https://localhost:44393/"));
            //services.AddHttpClient("default", client => client.BaseAddress = new Uri("https://localhost:444/"));

            services.AddTransient<IPublisherClientService, PublisherClientService>();
            services.AddTransient<ICategoryClientService, CategoryClientService>();
            services.AddTransient<IBookClientService, BookClientService>();
            services.AddTransient<IStoreClientService, StoreClientService>();
            services.AddTransient<IPropertiesService, PropertiesService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
