using System.Text;
using App.Config;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace App.Services.Security.Extensions
{
    public static class SecurityExtensions
    {
        public static IServiceCollection AddSecurity(this IServiceCollection services, IConfiguration config)
        {
            var audience = config["JWTSettings:Audience"];
            var issuer = config["JWTSettings:Issuer"];
            var secretKey = config["JWTSettings:SecretKey"];

            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));
            
            var tokenValidationParameters = new TokenValidationParameters() 
            {
                // Signing key validation
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,

                // Issuer validation
                ValidateIssuer = true,
                ValidIssuer = issuer,

                // Audience validation
                ValidateAudience = true,
                ValidAudience = audience,
                
                // Lifetime validation
                ValidateLifetime = true
            };

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(opt =>  
            {
                opt.TokenValidationParameters = tokenValidationParameters;
            });

            return services;
        }
    }
}