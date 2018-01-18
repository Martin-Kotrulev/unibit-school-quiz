namespace Uniquizbit.Controllers
{
  using Common.Config;
  using Data.Models;
  using System;
  using System.Collections.Generic;
  using System.Net;
  using System.Security.Claims;
  using System.Threading.Tasks;
  using Microsoft.AspNetCore.Authorization;
  using Microsoft.AspNetCore.Identity;
  using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.Extensions.Options;
  using Newtonsoft.Json;
  using Web.Models;

  public class UsersController : Controller
  {
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly JWTSettings _options;

    public UsersController(UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        RoleManager<IdentityRole> roleManager,
        IOptions<JWTSettings> optionsAccessor)
    {
      _userManager = userManager;
      _signInManager = signInManager;
      _roleManager = roleManager;
      _options = optionsAccessor.Value;
    }

    [HttpPost("[action]")]
    [AllowAnonymous]
    public async Task<IActionResult> SignUp([FromBody] RegisterResource credentials)
    {
      if (ModelState.IsValid)
      {
        var user = new ApplicationUser() {
          UserName = credentials.Username,
          Email = credentials.Email
        };

        var result = await _userManager.CreateAsync(user, credentials.Password);

        if (result.Succeeded)
        {
          var regUser = await _userManager
            .FindByNameAsync(credentials.Username);

          return Ok(new ApiResponse(new TokenResource()
          {
            User = user.UserName,
            UserId = user.Id,
            Token = GetToken(user),
            Expires = DateTime.UtcNow.AddDays(_options.ExpirationDays)
          }, "You have successfully registered and logged in."));
        }

        return BadRequest(new ApiResponse(result));
      }

      return BadRequest(new ApiResponse(ModelState));
    }

    [HttpPost("[action]")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] CredentialsResource credentials)
    {
      if (ModelState.IsValid)
      {
        var token = await _authService.SignInUserAsync(credentials);

        if ((token.AccessToken ?? token.IdToken ?? token.Token) != null)
        {
          return Ok(new ApiResponse(token, "You have successfully logged in."));
        }

        return BadRequest(new ApiResponse("Wrong user name or password.", false));
      }

      return BadRequest(new ApiResponse(ModelState));
    }

    public async Task<TokenResource> SignInUserAsync(CredentialsResource credentials)
    {
      var result = await _signInManager
        .PasswordSignInAsync(credentials.Username, credentials.Password, false, false);

      

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