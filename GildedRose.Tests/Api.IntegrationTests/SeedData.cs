using GildedRose.Data;
using GildedRose.Data.Models;

namespace GildedRose.Tests.Api.IntegrationTests
{
    public static class SeedData
    {
        public static void PopulateTestData(ApplicationDbContext dbContext)
        {
            dbContext.Products.Add(new Product { Id = 1, Description = "product1", Name = "product1", Price = 1, QuantityInHand = 10 });
            dbContext.Products.Add(new Product { Id = 2, Description = "product2", Name = "product2", Price = 2 , QuantityInHand = 1});
            dbContext.Products.Add(new Product { Id = 3, Description = "product3", Name = "product3", Price = 3, QuantityInHand = 0 });
            dbContext.SaveChanges();
        }
    }
}
 