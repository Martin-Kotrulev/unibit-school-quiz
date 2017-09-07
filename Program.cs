using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using App.Persistence;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace App
{
  public class Program
  {
    public static void Main(string[] args)
    {
      BuildWebHost(args)
        .MigrateDatabase()
        .Run();

      
    }

    public static IWebHost BuildWebHost(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>()
            .Build();
  }

  public static class MigrationConfig
  {
    public static IWebHost MigrateDatabase(this IWebHost webHost)
    {
        using (var scope = webHost.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var dbContext = services.GetRequiredService<AppDbContext>();

            dbContext.Database.Migrate();
        }

        return webHost;
    }
  }
}
