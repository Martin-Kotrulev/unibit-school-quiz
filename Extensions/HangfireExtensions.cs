using App.Persistence;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace App.Extensions
{
    public static class HangfireExtensions
    {
        public static IWebHost RecurringJobs(this IWebHost webHost)
        {
            RecurringJob.AddOrUpdate(() => 
                    System.Console.WriteLine("Recurring"), Cron.MinuteInterval(1));

            using (var scope = webHost.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var dbContext = services.GetRequiredService<AppDbContext>();

                
            }

            return webHost;
        }
    }
}