using GildedRose.Data;
using GildedRose.Data.Models;

namespace GildedRose.Tests.Api.IntegrationTests
{
    public static class SeedData
    {
        public static void PopulateTestData(ApplicationDbContext dbContext)
        {
            dbContext.Products.Add(new Product() { Id = 1, Description = "test", Name = "test", Price = 12 });
            dbContext.Products.Add(new Product() { Id = 2, Description = "t2est", Name = "est", Price = 132 });
            dbContext.SaveChanges();
        }
    }
}
 