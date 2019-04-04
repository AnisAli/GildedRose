using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GildedRose.API.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GildedRose.Controllers
{
    [ApiVersion("1.0")]
    [Route("/api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        // GET api/values
        [HttpGet("/products/list")]
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            return null;
        }

        // GET api/values/5
        [HttpGet("/product/{id}")]
        public ActionResult<Product> GetProductById(int id)
        {
            return new Product();
        }

    }
}
