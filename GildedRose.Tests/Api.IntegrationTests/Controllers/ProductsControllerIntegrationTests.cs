using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xunit;
using GildedRose;
using GildedRose.ViewModels;
using System;
using FluentAssertions;

namespace GildedRose.Tests.Api.IntegrationTests.Controllers
{
    public class ProductsControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public ProductsControllerIntegrationTests(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task CanGetProducts()
        {
            // The endpointof the controller action.
            var httpResponse = await _client.GetAsync("/api/v1/store/products");

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var productList = JsonConvert.DeserializeObject<ProductsPagedList>(stringResponse);
            Assert.Equal(2, productList.TotalProducts);
        }

        [Fact]
        public async Task CanGetProducts_WithInvalidPageSize()
        {
            // The endpointof the controller action.
            var pageSize = -10;
            var url = $"/api/v1/store/products?pageSize={pageSize}";
            var httpResponse = await _client.GetAsync(url);
            //act
            Action act = () => httpResponse.EnsureSuccessStatusCode();

            // assert
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
             stringResponse.Should().Contain("PageSize cannot be negative");
             act.Should().Throw<HttpRequestException>();
        }
    }
}
