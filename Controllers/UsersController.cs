using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using App.Controllers.Resources;
using App.Models;
using App.Services.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace App.Controllers
{
  [Route("api/[controller]")]
  public class UsersController : Controller
  {
    private readonly IAuthenticationService _authService;

    public UsersController(IAuthenticationService authService)
    {
      _authService = authService;
    }

    [HttpPost("[action]")]
    [AllowAnonymous]
    public async Task<IActionResult> SignUp([FromBody] RegisterResource credentials)
    {
      if (ModelState.IsValid)
      {
        var user = new ApplicationUser() { UserName = credentials.Username, Email = credentials.Email };
        var result = await _authService.RegisterUserAsync(user, credentials.Password);

        if (result.Succeeded)
        {
          var token = await _authService.SignInUserAsync(credentials);

          return Ok(new ApiResponse
          (
              token,
              "You have successfully registered and logged in."
          ));
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
  }
}