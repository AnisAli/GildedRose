using System.Threading;
using System.Threading.Tasks;
using GildedRose.ViewModels;
using Microsoft.AspNetCore.Mvc;
using GildedRose.API.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using GildedRose.API.ViewModels;
using AspNet.Security.OAuth.Validation;

namespace GildedRose.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly IProductService productService;


        public StoreController(IProductService productService)
        {
           this.productService = productService;
        }

  
        [HttpGet("products")]
        public async Task<ActionResult<ProductsPagedListVM>> GetProductsAsync([FromQuery] QueryParams request,
            CancellationToken cancellationToken)
        {
            return await productService.GetProductListAsync(cancellationToken,request.PageSize, request.PageNumber, request.SearchText);
        }


        [HttpPost("checkout")]
        [Authorize(AuthenticationSchemes = OAuthValidationDefaults.AuthenticationScheme)]
        public async Task<ActionResult<OrderVM>> CheckoutAsync([FromBody] OrderItem order, CancellationToken cancellationToken)
        {
           return await productService.CheckoutAsync(cancellationToken, order);
        }

    }
}
