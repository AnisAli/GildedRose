using GildedRose.API.Configurations;
using GildedRose.API.Middlewares.GlobalErrorHandling.Extenstions;
using GildedRose.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GildedRose
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
            services.ConfigureServices();

            services.AddDbContext<ApplicationDbContext>(opt =>
                opt.UseInMemoryDatabase("GildedRoseDB"));

           services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
       
           services.AddApiVersioning(options => options.ReportApiVersions = true);
           services.ConfigureAuthentication(Configuration);

        }

        // Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ApplicationDbContext dbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
         
            app.ConfigureExceptionHandler();

            app.UseAuthentication();

            app.UseMvc();
        }

    }
}
