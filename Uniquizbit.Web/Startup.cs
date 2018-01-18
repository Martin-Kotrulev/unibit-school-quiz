namespace Uniquizbit
{
  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Linq;
  using System.Net;
  using System.Text;
  using System.Threading.Tasks;
  using Common.Config;
  using Controllers;
  using Data;
  using Uniquizbit.Persistence;
  using Uniquizbit.Persistence.Repositories;
  using Uniquizbit.Persistence.Repositories.Interfaces;
  using Uniquizbit.Services;
  using Uniquizbit.Extensions;
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

      services.AddCors();

      // DbContext
      services.AddDbContext<AppDbContext>(opt =>
      {
        opt.UseNpgsql(connectionString: Configuration.GetConnectionString("Default"));
      });

      // Hangfire scheduler
      // services.AddHangfire(config => {
      //   config.UsePostgreSqlStorage(Configuration.GetConnectionString("Default"));
      // });

      // Identity
      services.AddIdentityService();

      // Applications services
      services.AddScoped<IUnitOfWork, UnitOfWork>();

      services.AddTransient<IAuthenticationService, AuthenticationService>();
      services.AddTransient<IQuizService, QuizService>();

      services.AddSecurity(configuration: Configuration);

      services.AddAutoMapper();

      services.AddMvc();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
