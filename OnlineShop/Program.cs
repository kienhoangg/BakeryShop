using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OnlineShop_Data.EF;
using System;

namespace OnlineShop
{
    public class Program
    {
        public static void Main(string[] args)
        {
            
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var service = scope.ServiceProvider;
                try
                {                  
                   var context = service.GetService<OnlineShopDbContext>();
                    context.Database.EnsureCreated();
                    var initializer = service.GetService<OnlineShopDbInitializer>();
                    initializer.Seed().Wait();
                }
                catch(Exception ex)
                {
                    var log = service.GetService<ILogger<Program>>();
                    log.LogError(ex, "An error occurred while seeding the database");
                }            
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
