using GildedRose.ViewModels;
using GildedRose.API.Services.Contracts;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using GildedRose.Data;
using GildedRose.Data.Models;
using System.Threading;
using GildedRose.API.ViewModels;
using System;

namespace GildedRose.API.Services.Implementation
{
    public class ProductService : IProductService
    {

        private readonly ApplicationDbContext _context;
        public ProductService(ApplicationDbContext context) => _context = context;

        public async Task<ProductsPagedListVM> GetProductListAsync(CancellationToken cancellationToken,int pageSize, int pageNo, string searchText)
        {

            cancellationToken.ThrowIfCancellationRequested();
            searchText = searchText?.ToLower().Trim() ?? string.Empty;
            pageSize = pageSize <= 0 ? 1000 : pageSize;
            pageNo = pageNo <= 0 ? 1 : pageNo;
            var emptySearchCriteria = string.IsNullOrWhiteSpace(searchText);

            var products = await _context.Products
                .Where(c => emptySearchCriteria || c.Name.ToLower().Contains(searchText))
                                    .OrderBy(c=>c.Id)
                                    .Skip((pageNo - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToListAsync(cancellationToken);

            return new ProductsPagedListVM { Products = ConvertProductVM(products), TotalProducts = products.Count };
        }

        public async Task<OrderVM> CheckoutAsync(CancellationToken cancellationToken, OrderItem orderItem)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var product = await _context.Products.Where(c => c.Id == orderItem.ProductId)
                .SingleOrDefaultAsync(cancellationToken);

           if (product == null)
           {
                throw new NotFoundException("Product Not Found!");
           }

           if (product.QuantityInHand < orderItem.Quantity)
           {
                throw new OutOfStockException(product.Id);
           }

            product.QuantityInHand -= orderItem.Quantity;
            _context.SaveChanges();

            return new OrderVM
            {
                OrderId = System.Guid.NewGuid(),
                TimeStamp = DateTime.UtcNow,
                Product = new ProductVM
                {
                                Name = product.Name,
                                ProductId = product.Id,
                                Description = product.Description,
                                Price = product.Price
                            }
            };

        }
        
        //we can use automapper to map ORM Object to VM
        private IEnumerable<ProductVM> ConvertProductVM(IEnumerable<Product> products)
        {
            return products.Select(dbModel => new ProductVM
                {
                    Name = dbModel.Name, ProductId = dbModel.Id, Description = dbModel.Description,
                    Price = dbModel.Price
                })
                .ToList();
        }

    }
}
