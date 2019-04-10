using GildedRose.Common.ViewModels;
using GildedRose.Services.Contracts;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using GildedRose.Data;
using GildedRose.Data.Models;
using System.Threading;

namespace GildedRose.Services.Implementation
{
    public class ProductService : IProductService
    {

        private readonly ApplicationDbContext _context;

        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ProductsPagedList> GetProductList(int PageSize, int PageNo, string Filter, CancellationToken cancalletiontToken)
        {
            var products = await _context.Products.ToListAsync(cancalletiontToken);
            return new ProductsPagedList() { Products = ConvertProductVM(products), TotalProducts = products.Count };
        }

        private IList<ProductVM> ConvertProductVM(IList<Product> products)
        {
            var list = new List<ProductVM>();

            foreach (var dbmodel in products)
            {
                list.Add(new ProductVM() { Name = dbmodel.Name, ProductId = dbmodel.Id, Description = dbmodel.Description, Price = dbmodel.Price });
            }

            return list;
        }

    }
}
