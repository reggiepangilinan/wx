using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;

namespace Wx.Exercises.Api
{
    public class Program
    {
        public static int Main(string[] args)
        {
            var loggerConfiguration = LoggingConfiguration.ConfigureLogger();
            Log.Logger = loggerConfiguration.CreateLogger();

            try
            {
                Log.Information("Starting web host");
                CreateHostBuilder(args).Build().Run();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureAppConfiguration((context, config) => {
                    config.AddEnvironmentVariables();
                    var env = context.HostingEnvironment;
                    Log.Information($"Current environment is {env.EnvironmentName}");

                    if (string.IsNullOrEmpty(env.EnvironmentName) || string.Equals(env.EnvironmentName, "development", StringComparison.CurrentCultureIgnoreCase))
                    {
                        config
                            .AddJsonFile("appsettings.json", optional: true);
                    }
                    else
                    {
                        config
                            .AddJsonFile("appsettings.json", false, true)
                            .AddJsonFile($"appsettings.{env.EnvironmentName.ToLower()}.json", optional: true, true);
                    }
                });
    }
}
