using GildedRose.Common.ViewModels;
using GildedRose.Services.Contracts;
using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;

namespace GildedRose.Services.Implementation
{
    public class ProductService : IProductService
    {
        public async Task<ProductsPagedList> GetProductList(int PageSize, int PageNo, string Filter)
        {
            return new ProductsPagedList() { Products = new List<Product>(), TotalProducts = 10 };
        }
    }
}
