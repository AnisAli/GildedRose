using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xunit;
using GildedRose;
using GildedRose.ViewModels;
using System;
using FluentAssertions;
using GildedRose.API.ViewModels;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Net;
using GildedRose.Api.Services.Contracts;
using GildedRose.Data;

namespace GildedRose.Tests.Api.IntegrationTests.ServiceTests
{

    public class ProductServiceTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        private readonly IProductService _productService; 

        public ProductServiceTest(IProductService productService)
        {
            //var applicationDbContext = new ApplicationDbContext(options =>
            //{
            //    options.UseInMemoryDatabase("InMemoryAppDb");
            //    options.UseInternalServiceProvider(serviceProvider);
            //});

            _productService = productService;
        }

        [Fact]
        public async Task CanGetProducts()
        {
            var expected = new ProductsPagedListVM()
            {
                TotalProducts = 3,
                Products = new List<ProductVM>()
              {
                  new ProductVM() { ProductId = 1, Description = "product1", Name = "product1", Price = 1},
                  new ProductVM() { ProductId = 2, Description = "product2", Name = "product2", Price = 2},
                  new ProductVM() { ProductId = 3, Description = "product3", Name = "product3", Price = 3}
              }
            };

            // The endpoint of the controller action.
            var httpResponse = await _productService.GetProductListAsync(new System.Threading.CancellationToken(), 0, 0, null);

            // Must be successful.
            //httpResponse.EnsureSuccessStatusCode();
            //// Deserialize and examine results.
            //var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            //var result = JsonConvert.DeserializeObject<ProductsPagedListVM>(stringResponse);
            httpResponse.Should().BeEquivalentTo(expected);
        }

    }


}
