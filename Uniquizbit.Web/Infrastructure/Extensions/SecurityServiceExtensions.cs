namespace Uniquizbit.Extensions
{
  using Microsoft.AspNetCore.Authentication.JwtBearer;
  using Microsoft.Extensions.Configuration;
  using Microsoft.Extensions.DependencyInjection;
  using Microsoft.IdentityModel.Tokens;
  using System.Text;

  public static class SecurityExtensions
  {
    public static IServiceCollection AddSecurity(
      this IServiceCollection services,
      IConfiguration configuration)
    {
      var audience = configuration["JWTSettings:Audience"];
      var issuer = configuration["JWTSettings:Issuer"];
      var secretKey = configuration["JWTSettings:SecretKey"];

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

      services
        .AddAuthentication(opt =>
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