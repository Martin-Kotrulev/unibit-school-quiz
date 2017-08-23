using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using App.Persistence;
using App.Middleware;
using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using App.Services.Security.Extensions;
using App.Config;
using App.Services.Security;

namespace WebApplicationBasic
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add JWT token configurations
            services.Configure<JWTSettings>(Configuration.GetSection("JWTSettings"));

            // Add framework services.
            // Custom extension method for Identity configuration
            services.AddIdentityService();

            // Custom authentication service
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            
            services.AddMvc();

            // Configuring the DbContext
            services.AddDbContext<AppDbContext>(
                options => options.UseNpgsql(
                    connectionString: Configuration.GetConnectionString("Default")
                )
            );
            services.AddAutoMapper();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                // Uncomment in case of webpack usage

                //app.UseDeveloperExceptionPage();
                // app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions {
                //     HotModuleReplacement = true
                // });
            }

            // Custom exception handler
            app.UseExceptionHandler(err => {
                err.Run(async context =>
                {
                    context.Response.ContentType = "application/json";

                    var error = context.Features.Get<IExceptionHandlerFeature>();
                    if (error != null)
                    {
                        var ex = error.Error;

                        await context.Response.WriteAsync(
                            JsonConvert.SerializeObject(new {
                                StatusCode = context.Response.StatusCode,
                                Message = ex.Message,
                                Success = false,
                            })
                        );
                    }
                });
            });

            app.UseStaticFiles();

            app.UseIdentity();

            //app.UserJsonResponseWrapper();
            
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action}/{id?}");
            });
        }
    }
}
