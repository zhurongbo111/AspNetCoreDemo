using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoreLocalization
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddLocalization(localizationOption => {
                localizationOption.ResourcesPath = "MyResources";
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var supportedCultures = new[] { "zh-CN", "en-US" };
            app.UseRequestLocalization((requestLocalizationOptions) =>
            {
                requestLocalizationOptions.RequestCultureProviders.Clear();
                requestLocalizationOptions.RequestCultureProviders.Add(new CookieRequestCultureProvider() { Options = requestLocalizationOptions });
                requestLocalizationOptions.AddSupportedCultures(supportedCultures);
                requestLocalizationOptions.AddSupportedUICultures(supportedCultures);
                requestLocalizationOptions.SetDefaultCulture(supportedCultures[0]);

                //Optionally create an app-specific provider with just a delegate, e.g. look up user preference from DB.
                //requestLocalizationOptions.RequestCultureProviders.Insert(0, 
                //    new CustomRequestCultureProvider(
                //        async context =>{
                //            return await Task.FromResult(new ProviderCultureResult("zh-CN"));
                //        }
                //    )
                //);

            });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
