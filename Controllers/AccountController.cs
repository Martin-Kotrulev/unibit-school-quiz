using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using App.Controllers.Resources;
using App.Models;
using App.Services.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IAuthenticationService _authService; 
        public AccountController(IAuthenticationService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IActionResult> Register([FromBody] CredentialsResource credentials)
        {
            if (ModelState.IsValid) {
                var user = new ApplicationUser() { UserName = credentials.Email, Email = credentials.Email };
                var result = await _authService.RegisterUserAsync(user, credentials.Password);

                if (result.Succeeded) {
                    var token = await _authService.SignInUserAsync(credentials);
                    
                    return Ok(new ApiResponse(
                        token,
                        $"User '{token.Username}' registered successfully."
                    ));
                }

                return BadRequest(new ApiResponse(result));
            }

            return BadRequest(new ApiResponse(ModelState));
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] CredentialsResource credentials)
        {
            if (ModelState.IsValid) {
                var token = await _authService.SignInUserAsync(credentials);
                    
                if ((token.AccessToken ?? token.IdToken ?? token.Token) != null)
                {
                    return Ok(new ApiResponse(token, $"User '{token.Username}' logged successfully."));
                }

                return BadRequest(new ApiResponse(
                    (int)HttpStatusCode.Unauthorized, 
                    "Wrong username or password.")
                );
            }

            return BadRequest(new ApiResponse(ModelState));
        }
    }
}