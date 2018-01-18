namespace Uniquizbit.Extensions
{
  using Data;
  using Data.Models;
  using Microsoft.AspNetCore.Builder;
  using Microsoft.AspNetCore.Identity;
  using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
  using Microsoft.Extensions.DependencyInjection;
  using Microsoft.AspNetCore.Authentication.Cookies;
  using Persistence;
  using System;
  using System.Net;
  using System.Threading.Tasks;  

  public static class IdentityServiceExtensions
  {
    public static IdentityBuilder AddIdentityService(
        this IServiceCollection services)
    {
      services.Configure<IdentityOptions>(options =>
      {
        // Password settings
        options.Password.RequireDigit = false;
        options.Password.RequiredLength = 4;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = false;

        // Lockout settings
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
        options.Lockout.MaxFailedAccessAttempts = 10;

        // User settings
        options.User.RequireUniqueEmail = true;
      });

      return services.AddIdentity<User, IdentityRole>()
          .AddEntityFrameworkStores<UniquizbitDbContext>()
          .AddDefaultTokenProviders();
    }
  }
}