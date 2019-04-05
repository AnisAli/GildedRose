using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GildedRose.Common.ViewModels;
using Microsoft.AspNetCore.Mvc;
using GildedRose.Services.Contracts;

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
        public async Task<ActionResult<ProductsPagedList>> GetProducts([FromQuery] QueryParams request, CancellationToken cancellationToken)
        {
            return await productService.GetProductList(1, 2, "a") ;
        }

        // GET api/v1/store/products/{id}
        [HttpGet("product/{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id, CancellationToken cancellationToken)
        {
            return new Product();
        }

        // POST api/v1/store/checkout
        [HttpPost("checkout")]
        public async Task<ActionResult<Product>> Checkout(int id, CancellationToken cancellationToken)
        {
            return new Product();
        }

    }
}
