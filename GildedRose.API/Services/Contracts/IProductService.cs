using System;
using System.Collections.Generic;
using System.Text;
using GildedRose.ViewModels;
using System.Threading;
using System.Threading.Tasks;
using GildedRose.API.ViewModels;

namespace GildedRose.Api.Services.Contracts
{
    public interface IProductService
    {
        Task<ProductsPagedListVM> GetProductListAsync(CancellationToken cancalletiontToken,int PageSize, int PageNo, string Filter);

        Task<OrderVM> CheckoutAsync(CancellationToken cancalletiontToken,OrderItem orderItem);

    }
}
