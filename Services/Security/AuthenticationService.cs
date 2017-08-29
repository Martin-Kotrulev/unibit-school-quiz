using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;
using App.Config;
using App.Controllers.Resources;
using App.Models;
using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace App.Services.Security
{
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

        public async Task<IdentityResult> RegisterUserAsync(ApplicationUser user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<TokenResource> SignInUserAsync(CredentialsResource credentials)
        {
            var result = await _signInManager
                .PasswordSignInAsync(credentials.Email, credentials.Password, false, false);
            
            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(credentials.Email);

                return new TokenResource() {
                    Username = user.UserName ?? user.Email,
                    Token = GetToken()
                };
            }

            return new TokenResource();
        }

        private string GetToken(Dictionary<string, object> payload = null)
        {
            var symmetricKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_options.SecretKey));

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Audience = _options.Audience,
                Issuer = _options.Issuer,
                IssuedAt = DateTime.UtcNow,
                NotBefore = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddDays(_options.ExpirationDays),
                SigningCredentials = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256)
            };

            var handler = new JwtSecurityTokenHandler();

            return handler.CreateEncodedJwt(tokenDescriptor);
        }
  }
}