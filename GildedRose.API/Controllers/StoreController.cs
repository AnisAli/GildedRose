using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GildedRose.ViewModels;
using Microsoft.AspNetCore.Mvc;
using GildedRose.Api.Services.Contracts;
using GildedRose.API.Helper.Attributes;
using Microsoft.AspNetCore.Identity;
using GildedRose.Data.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.Text;
using GildedRose.API.ViewModels;

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
        public async Task<ActionResult<ProductsPagedListVM>> GetProductsAsync([FromQuery] QueryParams request, CancellationToken cancellationToken)
        {
            return await productService.GetProductListAsync(cancellationToken,request.PageSize, request.PageNumber, request.SearchText);
        }


        [HttpPost("checkout")]
        [Authorize]
        public async Task<ActionResult<OrderVM>> CheckoutAsync([FromBody] OrderItem order, CancellationToken cancellationToken)
        {
           return await productService.CheckoutAsync(cancellationToken, order);
        }

    }
}
