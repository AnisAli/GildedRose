using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xunit;
using GildedRose.ViewModels;
using System;
using FluentAssertions;
using GildedRose.API.ViewModels;
using System.Net.Http.Headers;

namespace GildedRose.Tests.Api.IntegrationTests.Controllers
{
    public class ProductsControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory<Startup> _factory;
        //setup
        public  ProductsControllerIntegrationTests(CustomWebApplicationFactory<Startup> factory)
        {        
            _factory = factory;
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAllProductsTest()
        {
            var expected = new ProductsPagedListVM
            {
              TotalProducts = 3,
              Products = new List<ProductVM>
              {
                  new ProductVM { ProductId = 1, Description = "product1", Name = "product1", Price = 1},
                  new ProductVM { ProductId = 2, Description = "product2", Name = "product2", Price = 2},
                  new ProductVM { ProductId = 3, Description = "product3", Name = "product3", Price = 3}
              }
            };

            // The endpoint of the controller action.
            string stringResponse;
            using (var httpResponse = await _client.GetAsync("/api/v1/store/products"))
            {
                httpResponse.EnsureSuccessStatusCode();
                // Deserialize and examine results.
                stringResponse = await httpResponse.Content.ReadAsStringAsync();
            }

            var result = JsonConvert.DeserializeObject<ProductsPagedListVM>(stringResponse);
            result.Should().BeEquivalentTo(expected);
        }


        [Fact]
        public async Task GetAllProductsTest_WithPaging()
        {

            var pageNo = 2;
            var pageSize = 1; 
            var expected = new ProductsPagedListVM
            {
                TotalProducts = 1,
                Products = new List<ProductVM>
                {
                  new ProductVM { ProductId = 2, Description = "product2", Name = "product2", Price = 2},
              }
            };

            // The endpoint of the controller action.
            var url = $"/api/v1/store/products?pageSize={pageSize}&pageNumber={pageNo}";
            string stringResponse;
            using (var httpResponse = await _client.GetAsync(url))
            {
                httpResponse.EnsureSuccessStatusCode();
                // Deserialize and examine results.
                stringResponse = await httpResponse.Content.ReadAsStringAsync();
            }

            var result = JsonConvert.DeserializeObject<ProductsPagedListVM>(stringResponse);
            result.Should().BeEquivalentTo(expected);
        }


        [Fact]
        public async Task GetAllProductsTest_WithSearchProductName()
        {

            var searchText = "product3";
            var expected = new ProductsPagedListVM
            {
                TotalProducts = 1,
                Products = new List<ProductVM>
                {
                  new ProductVM { ProductId = 3, Description = "product3", Name = "product3", Price = 3},
              }
            };

            // The endpoint of the controller action.
            var url = $"/api/v1/store/products?searchText={searchText}";
            string stringResponse;
            using (var httpResponse = await _client.GetAsync(url))
            {
                httpResponse.EnsureSuccessStatusCode();
                // Deserialize and examine results.
                stringResponse = await httpResponse.Content.ReadAsStringAsync();
            }

            var result = JsonConvert.DeserializeObject<ProductsPagedListVM>(stringResponse);
            result.Should().BeEquivalentTo(expected);
        }


        [Fact]
        public async Task GetAllProductsTest_WithInvalidProductNameSearch()
        {

            const string searchText = "--";
            var expected = new ProductsPagedListVM
            {
                TotalProducts = 0,
                Products = new List<ProductVM>()
            };

            // The endpoint of the controller action.
            var url = $"/api/v1/store/products?searchText={searchText}";
            string stringResponse;
            using (var httpResponse = await _client.GetAsync(url))
            {
                httpResponse.EnsureSuccessStatusCode();
                // Deserialize and examine results.
                stringResponse = await httpResponse.Content.ReadAsStringAsync();
            }

            var result = JsonConvert.DeserializeObject<ProductsPagedListVM>(stringResponse);
            result.Should().BeEquivalentTo(expected);
        }


        [Fact]
        public async Task ChekoutTest()
        {
            var body = new OrderItem
            {
                 ProductId = 1,
                Quantity = 2
            };

            var expected = new OrderVM
            {
                Product = new ProductVM
                {
                    ProductId = 1,
                    Name = "product1",
                    Description = "product1",
                    Price = 1

                }
            };

            var url = $"/api/v1/store/checkout";
            var accessToken = await GetAuthorizationHeader();
            _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", accessToken);

            string responseString;
            using (var response = await _client.PostAsJsonAsync<OrderItem>(url, body))
            {
                response.EnsureSuccessStatusCode();
                responseString = await response.Content.ReadAsStringAsync();
            }

            var result = JsonConvert.DeserializeObject<OrderVM>(responseString);
            result.Should().BeEquivalentTo(expected, option=> option.Excluding(c=>c.OrderId).Excluding(c=>c.TimeStamp));
        }

        [Fact]
        public async Task ChekoutTest_UnAuthorized()
        {
            var url = $"/api/v1/store/checkout";
            var body = new OrderItem
            {
                ProductId = 1,
                Quantity = 2
            };

           //no authorization header pass
            var response = await _client?.PostAsJsonAsync<OrderItem>(url, body);
            try
            {
                response.EnsureSuccessStatusCode();
                Assert.True(false, "This should have thrown an exception");
            }
            catch (Exception e)
            {
                e.Should().BeOfType<HttpRequestException>();
            }
        }

        private async Task<string> GetAuthorizationHeader()
        {
            var keyValues = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("username", "test"),
                new KeyValuePair<string, string>("password", "test"),
                new KeyValuePair<string, string>("grant_type", "password")
            };
            HttpResponseMessage response;
            using (var request = new HttpRequestMessage(HttpMethod.Post, "/api/v1/auth/connect/token"))
            {
                request.Content = new FormUrlEncodedContent(keyValues);
                response = await _client.SendAsync(request);
            }

            try
            {
                response.EnsureSuccessStatusCode();
                var stringResponse = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<AuthVM>(stringResponse);
                return result.Access_Token;
            }
            catch (Exception)
            {
                return string.Empty ;
            }
        }


    }
}
