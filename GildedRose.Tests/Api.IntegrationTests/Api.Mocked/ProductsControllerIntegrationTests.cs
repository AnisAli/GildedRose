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

namespace GildedRose.Tests.Api.IntegrationTests.Controllers
{
    public class ProductsControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory<Startup> _factory;
        public ProductsControllerIntegrationTests(CustomWebApplicationFactory<Startup> factory)
        {        
            _factory = factory;
            _client = factory.CreateClient();
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
            var httpResponse = await _client.GetAsync("/api/v1/store/products");

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();
            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ProductsPagedListVM>(stringResponse);
            result.Should().BeEquivalentTo(expected);
        }


        [Fact]
        public async Task CanGetProducts_WithPaging()
        {

            var pageNo = 2;
            var pageSize = 1; 
            var expected = new ProductsPagedListVM()
            {
                TotalProducts = 1,
                Products = new List<ProductVM>()
              {
                  new ProductVM() { ProductId = 2, Description = "product2", Name = "product2", Price = 2},
              }
            };

            // The endpoint of the controller action.
            var url = $"/api/v1/store/products?pageSize={pageSize}&pageNumber={pageNo}";
            var httpResponse = await _client.GetAsync(url);

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();
            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ProductsPagedListVM>(stringResponse);
            result.Should().BeEquivalentTo(expected);
        }


        [Fact]
        public async Task CanGetProducts_WithSearchProductName()
        {

            var searchText = "product3";
            var expected = new ProductsPagedListVM()
            {
                TotalProducts = 1,
                Products = new List<ProductVM>()
              {
                  new ProductVM() { ProductId = 3, Description = "product3", Name = "product3", Price = 3},
              }
            };

            // The endpoint of the controller action.
            var url = $"/api/v1/store/products?searchText={searchText}";
            var httpResponse = await _client.GetAsync(url);

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();
            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ProductsPagedListVM>(stringResponse);
            result.Should().BeEquivalentTo(expected);
        }


        [Fact]
        public async Task CanGetProducts_WithInvalidProductNameSearch()
        {

            var searchText = "--";
            var expected = new ProductsPagedListVM()
            {
                TotalProducts = 0,
                Products = new List<ProductVM>()
            };

            // The endpoint of the controller action.
            var url = $"/api/v1/store/products?searchText={searchText}";
            var httpResponse = await _client.GetAsync(url);

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();
            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ProductsPagedListVM>(stringResponse);
            result.Should().BeEquivalentTo(expected);
        }

    }
}
