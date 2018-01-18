using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Uniquizbit.Services
{
  using Common.Config;
  using Data.Models;

  public class AuthenticationService : IAuthenticationService
  {
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly JWTSettings _options;

    public AuthenticationService(UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        RoleManager<IdentityRole> roleManager,
        IOptions<JWTSettings> optionsAccessor)
    {
      _userManager = userManager;
      _signInManager = signInManager;
      _roleManager = roleManager;
      _options = optionsAccessor.Value;
    }

    public async Task<ApplicationUser> GetAuthenticatedUser(ClaimsPrincipal principal)
    {
      return await _userManager.GetUserAsync(principal);
    }

    public string GetAuthenticatedUserId(ClaimsPrincipal principal)
    {
      return _userManager.GetUserId(principal);
    }

    public async Task<IdentityResult> RegisterUserAsync(ApplicationUser user, string password)
    {
      return await _userManager.CreateAsync(user, password);
    }

    

    
  }
}