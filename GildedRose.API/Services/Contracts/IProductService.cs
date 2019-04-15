using GildedRose.ViewModels;
using System.Threading;
using System.Threading.Tasks;
using GildedRose.API.ViewModels;

namespace GildedRose.API.Services.Contracts
{
    public interface IProductService
    {
        Task<ProductsPagedListVM> GetProductListAsync(CancellationToken cancellationToken,int PageSize, int PageNo, string Filter);

        Task<OrderVM> CheckoutAsync(CancellationToken cancellationToken,OrderItem orderItem);

    }
}
