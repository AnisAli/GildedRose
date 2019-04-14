using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using GildedRose.API.Services.Contracts;

namespace GildedRose.API.Services.Implementation
{
    public class AuthenticationService : IAuthenticationService
    {

        private const string Username = "test";
        private const string Password = "test"; 

        public Task<IdentityResult> LoginAsync(string username, string password)
        {
            var result = username == Username && password == Password
                ? IdentityResult.Success
                : IdentityResult.Failed(new IdentityError() { Description = "Invalid Username or Password" });

            return Task.FromResult(result);
        }
    }
}
