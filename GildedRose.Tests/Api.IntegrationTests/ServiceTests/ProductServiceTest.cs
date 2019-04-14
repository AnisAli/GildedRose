using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xunit;
using GildedRose;
using GildedRose.ViewModels;
using System;
using FluentAssertions;
using GildedRose.API;
using GildedRose.API.ViewModels;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Net;
using GildedRose.API.Services.Contracts;
using GildedRose.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using GildedRose.API.Services.Implementation;

namespace GildedRose.Tests.Api.IntegrationTests.ServiceTests
{

    public class ProductServiceTest
    {
        private readonly IProductService _productService;
        private ApplicationDbContext _context;
        
        // SETUP
        public ProductServiceTest()
        {
            var serviceProvider = new ServiceCollection()
                  .AddEntityFrameworkInMemoryDatabase()
                  .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();

            builder.UseInMemoryDatabase("InMemoryAppDb")
                    .UseInternalServiceProvider(serviceProvider);
            _context = new ApplicationDbContext(builder.Options);

            
            SeedData.PopulateTestData(_context);

            _productService =  new ProductService(_context);
        }

        [Fact]
        public async Task GetAllProductTest()
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

            var result = await _productService.GetProductListAsync(new System.Threading.CancellationToken(), 0, 0, null);

            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task GetProductsTest_WithPaging()
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

            var result = await _productService.GetProductListAsync(new System.Threading.CancellationToken(), pageSize, pageNo, null);

            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task CanGetProductsTest_WithSearchProductName()
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

            var result = await _productService.GetProductListAsync(new System.Threading.CancellationToken(), 0, 0, searchText);

            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task CanGetProductsTest_WithInvalidProductNameSearch()
        {

            var searchText = "--";
            var expected = new ProductsPagedListVM()
            {
                TotalProducts = 0,
                Products = new List<ProductVM>()
            };

            // The endpoint of the controller action.
            var result = await _productService.GetProductListAsync(new System.Threading.CancellationToken(), 0, 0, searchText);

            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task ProductCheckoutTest_OutofStock()
        {
           var orderItem = new OrderItem()
            {
                ProductId = 3,  // product 3 is out of stock as per test seed data
                Quantity = 1
            };

            try
            {
                await _productService.CheckoutAsync(new System.Threading.CancellationToken(), orderItem);
            }
            catch (Exception e)
            {
                e.Should().BeOfType<OutOfStockException>();
            }

        }

        [Fact]
        public async Task ProductChekout_InvalidProduct()
        {
            var orderItem = new OrderItem()
            {
                ProductId = 100,  
                Quantity = 1
            };

            try
            {
                await _productService.CheckoutAsync(new System.Threading.CancellationToken(), orderItem);
            }
            catch (Exception e)
            {
                e.Should().BeOfType<NotFoundException>();
            }
        }



    }


}
