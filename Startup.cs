using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using App.Config;
using App.Controllers;
using App.Models;
using App.Persistence;
using App.Persistence.Repositories;
using App.Persistence.Repositories.Interfaces;
using App.Services;
using App.Services.Security;
using App.Extensions;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using AutoMapper;

namespace App
{
  public class Startup
  {
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      // Custom configurations
      services.Configure<JWTSettings>(Configuration.GetSection("JWTSettings"));
      services.Configure<GradesSettings>(Configuration.GetSection("GradesSettings"));

      // DbContext
      services.AddDbContext<AppDbContext>(opt =>
      {
        opt.UseNpgsql(connectionString: Configuration.GetConnectionString("Default"));
      });

      // Hangfire scheduler
      services.AddHangfire(config => {
        config.UsePostgreSqlStorage(Configuration.GetConnectionString("Default"));
      });

      // Identity
      services.AddIdentityService();

      // Applications services
      services.AddScoped<IUnitOfWork, UnitOfWork>();

      services.AddTransient<IAuthenticationService, AuthenticationService>();
      services.AddTransient<IQuizService, QuizService>();

      services.AddSecurity(config: Configuration);

      // Redirection configuration
      services.ConfigureApplicationCookie(opt =>
      {
        opt.Events = new CookieAuthenticationEvents()
        {
          OnRedirectToLogin = ctx =>
                {
                ctx.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return Task.FromResult(0);
              }
        };
      });

      services.AddAutoMapper();

      services.AddMvc();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      //app.UseCustomExceptionHandler();
      if (env.IsDevelopment())
      {
        //app.UseDeveloperExceptionPage();
        app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
        {
          HotModuleReplacement = true
        });
      }
      else
      {
        app.UseExceptionHandler("/Home/Error");
      }

      app.UseHangfireServer();

      app.UseStatusCodePages();

      //Redirects not found status back to root if it's not an api call
      app.Use(async (context, next) =>
      {
        await next();
        
        if (context.Response.StatusCode == 404 &&
          !Path.HasExtension(context.Request.Path.Value) &&
          !context.Request.Path.Value.StartsWith("/api/"))
        {
          context.Request.Path = "/";
          await next();
        }
      });

      app.UseStaticFiles();

      app.UseAuthentication();

      app.UseMvcWithDefaultRoute();
    }
  }
}
