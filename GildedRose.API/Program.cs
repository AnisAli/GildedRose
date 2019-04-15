using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
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
                services.GetRequiredService<ApplicationDbContext>();

                //Call the DataGenerator to create sample data
                DataGenerator.Initialize(services);
            }

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseKestrel()
                .UseStartup<Startup>()
                .UseUrls("http://0.0.0.0:5000/");
    }
}
