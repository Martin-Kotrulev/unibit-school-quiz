using System.Threading.Tasks;
using App.Controllers.Resources;
using App.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace App.Services.Security
{
    public interface IAuthenticationService
    {   
        Task<IdentityResult> RegisterUserAsync(ApplicationUser user, string password);
        Task<TokenResource> SignInUserAsync(CredentialsResource credentials);
    }
}