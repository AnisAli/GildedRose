﻿using FluentValidation.AspNetCore;
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

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
           services.ConfigureServices();
           services.AddDbContext<ApplicationDbContext>(opt =>
                opt.UseInMemoryDatabase("GildedRoseDB"));
           services.AddMvc().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());

           services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
           services.AddApiVersioning(options => options.ReportApiVersions = true);
          
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.ConfigureExceptionHandler();
            app.UseMvc();
        }
    }
}
