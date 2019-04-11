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

namespace GildedRose.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    //[Route("api/[controller]")]
    [ApiController]


    public class StoreController : ControllerBase
    {
        private readonly IProductService productService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;

        public StoreController(IProductService productService, IConfiguration configuration, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
                this._userManager = userManager;
                this.productService = productService;
                this._signInManager = signInManager;
            this._configuration = configuration;

        }


        [HttpGet]
        public async Task<object> Login()
        {
            // var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

            //   if (result.Succeeded)
            //  {
            // var appUser = _userManager.Users.SingleOrDefault(r => r.Email == model.Email);
            return  await GenerateJwtToken("test@test.com", new ApplicationUser() { Id = 111 });
          //  }

            throw new ApplicationException("INVALID_LOGIN_ATTEMPT");
        }


        private async Task<object> GenerateJwtToken(string email, ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JwtExpireDays"]));

            var token = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtIssuer"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        // GET api/v1/store/products
        [HttpGet("products")]
        [ValidateModel]
        [Authorize]
        public async Task<ActionResult<ProductsPagedList>> GetProducts([FromQuery] QueryParams request, CancellationToken cancellationToken)
        {
            var user = new ApplicationUser()
            {
                Email = "aa@aa.com",
                Id = 1123,
                PasswordHash = "test",
                UserName = "test"

            };

            await this._userManager.CreateAsync(user);
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
