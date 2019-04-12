using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace GildedRose.Api.Services.Contracts
{
    public interface IAuthenticationService
    {
        Task<IdentityResult> LoginAsync(string username, string password);
    }
}
