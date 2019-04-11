using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GildedRose.ViewModels;
using Microsoft.AspNetCore.Mvc;
using GildedRose.Api.Services.Contracts;
using GildedRose.API.Helper.Attributes;
namespace GildedRose.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    //[Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly IProductService productService;
        public StoreController(IProductService productService)
        {
                this.productService = productService; ;
        }
        // GET api/v1/store/products
        [HttpGet("products")]
        [ValidateModel]
        public async Task<ActionResult<ProductsPagedList>> GetProducts([FromQuery] QueryParams request, CancellationToken cancellationToken)
        {
            return await productService.GetProductList(1, 2, "a", cancellationToken) ;
        }

        // GET api/v1/store/products/{id}
        [HttpGet("product/{id}")]
        public async Task<ActionResult<ProductVM>> GetProductById(int id, CancellationToken cancellationToken)
        {
            return new ProductVM();
        }

        // POST api/v1/store/checkout
        [HttpPost("checkout")]
        public async Task<ActionResult<ProductVM>> Checkout(int id, CancellationToken cancellationToken)
        {
            return new ProductVM();
        }

    }
}
