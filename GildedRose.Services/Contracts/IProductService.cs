using System;
using System.Collections.Generic;
using System.Text;
using GildedRose.Common.ViewModels;
using System.Threading;
using System.Threading.Tasks;

namespace GildedRose.Services.Contracts
{
    public interface IProductService
    {
        Task<ProductsPagedList> GetProductList(int PageSize, int PageNo, string Filter);
    }
}
