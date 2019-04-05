using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GildedRose.Services.Contracts;
using GildedRose.Services.Implementation;

namespace GildedRose.API.Configurations
{
    public static class ServiceConfiguration
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            //service
            services.AddScoped<IProductService, ProductService>();
 
        }

    }
}
