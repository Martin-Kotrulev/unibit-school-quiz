using System.Security.Claims;
using System.Threading.Tasks;
using Uniquizbit.Controllers.Resources;
using Uniquizbit.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Uniquizbit.Services
{
  public interface IAuthenticationService
  {
    Task<IdentityResult> RegisterUserAsync(ApplicationUser user, string password);
    Task<TokenResource> SignInUserAsync(CredentialsResource credentials);
    Task<ApplicationUser> GetAuthenticatedUser(ClaimsPrincipal principal);
    string GetAuthenticatedUserId(ClaimsPrincipal principal);
  }
}