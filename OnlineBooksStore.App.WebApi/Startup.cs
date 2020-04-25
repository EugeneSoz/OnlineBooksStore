using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineBooksStore.App.WebApi.Infrastructure;
using OnlineBooksStore.App.WebApi.Models.Database;
using OnlineBooksStore.Persistence.EF;

namespace OnlineBooksStore.App.WebApi
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment _environment;

        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            _configuration = configuration;
            _environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwagger(_environment);

            services.AddEntityFrameworkIdentity(_configuration);
            services.AddEntityFramework(_configuration);

            services.AddSession();
            services.AddAntiforgery(options => {
                options.HeaderName = "X-XSRF-TOKEN";
            });

            services.AddRepositories();

            services.AddCommandsAndQueries();

            services.AddServices();
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

            app.UseSwaggerWithUi();

            app.UseSpa(_configuration);

            IdentitySeedData.SeedDatabase(identityContext, userManager, roleManager).GetAwaiter().GetResult();
        }
    }
}
