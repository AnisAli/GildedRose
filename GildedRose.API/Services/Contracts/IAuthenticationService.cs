using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace GildedRose.API.Services.Contracts
{
    public interface IAuthenticationService
    {
        Task<IdentityResult> LoginAsync(string username, string password);
    }
}
