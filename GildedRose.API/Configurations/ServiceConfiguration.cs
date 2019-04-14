using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GildedRose.API.Services.Implementation;
using GildedRose.API.Services.Contracts;

namespace GildedRose.API.Configurations
{
    public static class ServiceConfiguration
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            //service
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
        }

    }
}
