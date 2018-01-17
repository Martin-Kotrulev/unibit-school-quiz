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
  using Uniquizbit.Config;
  using Uniquizbit.DTOs;
  using Uniquizbit.Models;

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

    public async Task<TokenResource> SignInUserAsync(CredentialsResource credentials)
    {
      var result = await _signInManager
        .PasswordSignInAsync(credentials.Username, credentials.Password, false, false);

      if (result.Succeeded)
      {
        var user = await _userManager.FindByNameAsync(credentials.Username);

        return new TokenResource()
        {
          User = user.UserName,
          UserId = user.Id,
          Token = GetToken(user),
          Expires = DateTime.UtcNow.AddDays(_options.ExpirationDays)
        };
      }

      return new TokenResource();
    }

    private string GetToken(ApplicationUser user)
    {
      var symmetricKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_options.SecretKey));

      var claimsIdentity = new ClaimsIdentity(new List<Claim>()
      {
          new Claim(ClaimTypes.Name, user.UserName),
          new Claim(ClaimTypes.Email, user.Email),
          new Claim(ClaimTypes.NameIdentifier, user.Id)
      });

      var tokenDescriptor = new SecurityTokenDescriptor()
      {
        Audience = _options.Audience,
        Issuer = _options.Issuer,
        IssuedAt = DateTime.UtcNow,
        NotBefore = DateTime.UtcNow,
        Subject = claimsIdentity,
        Expires = DateTime.UtcNow.AddDays(_options.ExpirationDays),
        SigningCredentials = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256)
      };

      var handler = new JwtSecurityTokenHandler();

      return handler.CreateEncodedJwt(tokenDescriptor);
    }
  }
}