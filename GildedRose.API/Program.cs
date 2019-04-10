using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using GildedRose.API.Helper;
using GildedRose.Data;
using Microsoft.Extensions.DependencyInjection;

namespace GildedRose
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Get the IWebHost
            var host = CreateWebHostBuilder(args).Build();

            //Find the service layer within scope.
            using (var scope = host.Services.CreateScope())
            {
                //Get the instance of ApplicationDbContext in the services layer
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<ApplicationDbContext>();

                //Call the DataGenerator to create sample data
                DataGenerator.Initialize(services);
            }

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseKestrel()
                .UseStartup<Startup>()
                .UseUrls("http://localhost:5000/");
    }
}
