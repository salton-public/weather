using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace WeatherApi
{
    public class Program
    {

        private ILoggerFactory loggerFactory;

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                 .ConfigureWebHostDefaults(webBuilder =>
                     webBuilder.ConfigureAppConfiguration(config =>
                     {

                         // Sentinel Key
                         // https://docs.microsoft.com/en-us/azure/azure-app-configuration/enable-dynamic-configuration-aspnet-core?tabs=core5x

                         var settings = config.Build();
                         var connectionString = settings["FeatureFlags:ConnectionString"];
                         const double defaultFeatureFlagsCacheInMilliseconds = 1000;

                         if (double.TryParse(settings["FeatureFlags:CacheInMilliseconds"], out var cacheInterval)) { }
                         else
                         {
                             cacheInterval = defaultFeatureFlagsCacheInMilliseconds;
                         }
                         if (!string.IsNullOrEmpty(connectionString))
                         {
                             config.AddAzureAppConfiguration(options =>
                             {
                                 options
                                     .Connect(connectionString)
                                     .UseFeatureFlags(opt => {
                                         opt.CacheExpirationInterval = TimeSpan.FromMilliseconds(cacheInterval);
                                     });
                             });
                         }
                     }).UseStartup<Startup>());
    }
}
