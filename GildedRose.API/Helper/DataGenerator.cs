using System;
using System.Linq;
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
                if (context.Products.Any())
                {
                    return;   // Data was already seeded
                }

                context.Products.AddRange(
                    new Data.Models.Product
                    {
                        Id = 1,
                        Name = "Blue Bags",
                        Description = "Fancy Blue Bags",
                        QuantityInHand = 3,
                        Price= 12
                    },
                     new Data.Models.Product
                     {
                         Id = 2,
                         Name = "Green T-Shirt",
                         Description = "Fancy Green T-Shirt",
                         QuantityInHand = 5,
                         Price = 100
                     },
                      new Data.Models.Product
                      {
                          Id = 3,
                          Name = "Yellow Book",
                          Description = "Yellow interestig Book",
                          QuantityInHand = 10,
                          Price = 56
                      }
                   );

                context.SaveChanges();
            }
        }
    }
}
