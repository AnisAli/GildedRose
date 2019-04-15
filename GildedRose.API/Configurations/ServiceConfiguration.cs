using Microsoft.Extensions.DependencyInjection;
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
        }

    }
}
