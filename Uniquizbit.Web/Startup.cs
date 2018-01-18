namespace Uniquizbit.Web
{
  using AutoMapper;
  using Common.Config;
  using Controllers;
  using Data;
  using Extensions;
  using Hangfire;
  using Hangfire.PostgreSql;
  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Linq;
  using System.Net;
  using System.Text;
  using System.Threading.Tasks;
  using Services;
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
  
  public class Startup
  {
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
      // Custom configurations
      services.Configure<JWTSettings>(Configuration.GetSection("JWTSettings"));
      services.Configure<GradesSettings>(Configuration.GetSection("GradesSettings"));

      services.AddCors();

      // DbContext
      services.AddDbContext<UniquizbitDbContext>(opt =>
      {
        opt.UseNpgsql(connectionString: Configuration.GetConnectionString("Default"));
      });

      // Hangfire scheduler
      // services.AddHangfire(config => {
      //   config.UsePostgreSqlStorage(Configuration.GetConnectionString("Default"));
      // });

      services.AddIdentityService();

      services.AddScoped<IQuizService, QuizService>();

      services.AddSecurity(configuration: Configuration);

      services.AddAutoMapper();

      services.AddMvc();
    }

    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      //app.UseHangfireServer();

      app.UseStatusCodePages();

      app.UseStaticFiles();

      // Configure cross origin resource sharing
      app.UseCors(
        options => options.WithOrigins("http://localhost:3000")
          .WithHeaders("authorization", "accept", "content-type", "origin", "x-custom-header")
          .AllowAnyMethod()
      );

      app.UseAuthentication();

      app.UseMvc();
    }
  }
}
