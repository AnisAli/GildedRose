using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GildedRose.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GildedRose.API.Helper
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                // Look for any board games.
                if (context.Products.Any())
                {
                    return;   // Data was already seeded
                }

                context.Products.AddRange(
                    new Data.Models.Product
                    {
                        Id = 1,
                        Name = "Pepsi",
                        Description = "Cola"
                    },
                     new Data.Models.Product
                     {
                         Id = 2,
                         Name = "Pepsi2",
                         Description = "Cola2"
                     }
                   );

                context.SaveChanges();
            }
        }
    }
}
