using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using App.Config;
using App.Controllers.Resources;
using App.Core.Models;
using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

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
                    AccessToken = GetAccessToken(user.Email),
                    IdToken = GetIdToken(user)
                };
            }

            return new TokenResource();
        }

        private string GetIdToken(IdentityUser user)
        {
            var payload = new Dictionary<string, object>()
            {
                { "id", user.Id },
                { "username", user.UserName },
                { "sub", user.Email },
                { "email", user.Email },
                { "emailConfirmed", user.EmailConfirmed }
            };

            return GetToken(payload);
        }

        private string GetAccessToken(string Email)
        {
            var payload = new Dictionary<string, object>()
            {
                { "sub", Email },
                { "email", Email }
            };

            return GetToken(payload);
        }

        private string GetToken(Dictionary<string, object> payload)
        {
            var secret = _options.SecretKey;

            payload.Add("iss", _options.Issuer);
            payload.Add("aut", _options.Audience);
            payload.Add("nbf", ConvertToUnixTimestamp(DateTime.Now));
            payload.Add("iat", ConvertToUnixTimestamp(DateTime.Now));
            payload.Add("exp", ConvertToUnixTimestamp(DateTime.Now.AddDays(_options.ExpirationDays)));

            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);

            return encoder.Encode(payload, secret);
        }

        private static double ConvertToUnixTimestamp(DateTime date)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan diff = date.ToUniversalTime() - origin;
            return Math.Floor(diff.TotalSeconds);
        }
  }
}