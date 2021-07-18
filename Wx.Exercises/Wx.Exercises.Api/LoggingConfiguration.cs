using Serilog;
using Serilog.Events;
using System;

namespace Wx.Exercises.Api
{
    /// <summary>
    /// LoggingConfiguration
    /// </summary>
    public static class LoggingConfiguration

    {
        /// <summary>
        /// Configure the application logger
        /// </summary>
        /// <returns>LoggerConfiguration</returns>
        public static LoggerConfiguration ConfigureLogger()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var loggerConfiguration = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext();

            loggerConfiguration.WriteTo.Console();
            return loggerConfiguration;
        }
    }
}
