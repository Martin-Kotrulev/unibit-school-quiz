using System.Collections.Generic;
using System.Threading.Tasks;
using App.Controllers.Resources;
using App.Core.Models;
using App.Services.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthenticationService _authService; 
        public AccountController(IAuthenticationService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [AllowAnonymous]
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

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] CredentialsResource credentials)
        {
            if (ModelState.IsValid) {
                var token = await _authService.SignInUserAsync(credentials);
                    
                if ((token.AccessToken ?? token.IdToken) != null)
                {
                    return Ok(new ApiResponse(token, $"User '{token.Username}' logged successfully."));
                }

                return BadRequest(new ApiResponse(400, "Wrong username or password."));
            }

            return BadRequest(new ApiResponse(ModelState));
        }
    }
}