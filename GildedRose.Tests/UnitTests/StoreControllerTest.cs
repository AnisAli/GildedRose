using FluentAssertions;
using GildedRose.API.Services.Contracts;
using GildedRose.API.Services.Implementation;
using GildedRose.Controllers;
using GildedRose.ViewModels;
using GildedRose.Data;
using Moq;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using System.Collections.Generic;
using GildedRose.API.ViewModels;
using System;

namespace GildedRose.Tests.UnitTests
{
    public class StoreControllerTest
    {

        private Mock<IProductService> productService;
        private StoreController controller;


        public StoreControllerTest()
        {
            productService = new Mock<IProductService>();
            controller = new StoreController(productService.Object);
        }

        [Fact]
        public async Task GetAllProductsAsync()
        {
            productService.Setup(s => s.GetProductListAsync(It.IsAny<CancellationToken>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()))
           .Returns(Task.FromResult(
                new ProductsPagedListVM()
                {
                    TotalProducts = 3,
                    Products = new List<ProductVM>()
                      {
                          new ProductVM() { ProductId = 1, Description = "product1", Name = "product1", Price = 1},
                          new ProductVM() { ProductId = 2, Description = "product2", Name = "product2", Price = 2},
                          new ProductVM() { ProductId = 3, Description = "product3", Name = "product3", Price = 3}
                      }
                })
            );

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


            var queryParam = new QueryParams()
            {
                PageNumber = 12,
                PageSize = 1,
                SearchText = "abc"
            };

            var result = await controller.GetProductsAsync(queryParam, new CancellationToken());
            result.Value.Should().BeEquivalentTo(expected);

        }

        [Fact]
        public async Task CheckoutAsync()
        {
            var guid = System.Guid.NewGuid();
            var timeStamp = DateTime.UtcNow;
            productService.Setup(s => s.CheckoutAsync(It.IsAny<CancellationToken>(), It.IsAny<OrderItem>()))
                .Returns(Task.FromResult(new OrderVM
                {
                    OrderId = guid,
                    TimeStamp = timeStamp,
                    Product = new ProductVM
                    {
                        ProductId = 1,
                        Name="Bag"
                    }
                }));

            var expected = new OrderVM()
            {
                OrderId = guid,
                TimeStamp = timeStamp,
                Product = new ProductVM
                {
                    ProductId = 1,
                    Name = "Bag"
                }
            };

            var result = await controller.CheckoutAsync(null,new CancellationToken());
            result.Value.Should().BeEquivalentTo(expected);

        }
    }
}
