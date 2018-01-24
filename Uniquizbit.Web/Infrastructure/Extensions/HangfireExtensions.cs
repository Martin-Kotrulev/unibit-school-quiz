namespace Uniquizbit.Extensions
{
  using Data;
  using Hangfire;
  using Microsoft.AspNetCore.Builder;
  using Microsoft.AspNetCore.Hosting;
  using Microsoft.Extensions.DependencyInjection;
  using Uniquizbit.Persistence;
  
  public static class HangfireExtensions
  {
    public static IWebHost RecurringJobs(this IWebHost webHost)
    {
      RecurringJob.AddOrUpdate(() =>
              System.Console.WriteLine("Recurring"), Cron.MinuteInterval(1));

      using (var scope = webHost.Services.CreateScope())
      {
        var services = scope.ServiceProvider;
        var dbContext = services.GetRequiredService<UniquizbitDbContext>();
      }

      return webHost;
    }
  }
}