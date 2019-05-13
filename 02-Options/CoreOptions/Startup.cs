using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreOptions.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoreOptions
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

            services.Configure<EmailOption>(op => op.Title = "Default Name");
            services.Configure<EmailOption>("FromMemory", op => op.Title= "FromMemory");
            services.Configure<EmailOption>("FromConfiguration", Configuration.GetSection("Email"));
            services.AddOptions<EmailOption>("AddOption").Configure(op => op.Title = "AddOption Title");

            services.Configure<EmailOption>(null, op => op.From = "Same With ConfigureAll");
            //services.ConfigureAll<EmailOption>(op => op.From = "ConfigureAll");

            services.PostConfigure<EmailOption>(null, op => op.Body = "Same With PostConfigureAll");
            //services.PostConfigureAll<EmailOption>(op => op.Body = "PostConfigurationAll");


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
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
