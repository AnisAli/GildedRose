using FluentValidation.AspNetCore;
using GildedRose.API.Configurations;
using GildedRose.API.Middlewares.GlobalErrorHandling.Extenstions;
using GildedRose.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System;
using GildedRose.Data.Models;
using GildedRose.API.AuthenticationProvider;
namespace GildedRose
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment envirnoment)
        {
            Configuration = configuration;
            env = envirnoment;
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment env { get; }


        public void ConfigureServices(IServiceCollection services)
        {
           services.ConfigureServices();
           services.ConfigureApplicationCookie(options => options.LoginPath = "/api/v1/auth");

            services.AddDbContext<ApplicationDbContext>(opt =>
                opt.UseInMemoryDatabase("GildedRoseDB"));

           services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
       
           services.AddApiVersioning(options => options.ReportApiVersions = true);
         //   if (!env.IsEnvironment("Test"))
         //   {
                services.ConfigureAuthentication(Configuration,env);
         //   }

        }

        // Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ApplicationDbContext dbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
         
            app.ConfigureExceptionHandler();

            if (!env.IsEnvironment("Test"))
            {
                app.UseAuthentication();
            }

            app.UseMvc();
        }
    }
}
