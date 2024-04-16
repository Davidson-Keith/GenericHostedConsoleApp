using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace GenericHostedConsoleApp;

/**
 * A simple demo Console App Hosted by the .NET Generic Host async.
 * Using the Generic Host's built in:
 *  - DI
 *  - Configuration
 *  - Logging (Using Serilog)
 *
 * From:
 * https://www.code4it.dev/blog/dependency-injection-config-logging-in-console-application/
 *
 * Modified by myself to:
 *  - Fill out Serilog setup, config, and usage.
 *  - Play with nested config settings. 
 */
class Program {
  static void Main(string[] args) {
    IHost host = CreateHost();
    Log.Logger.Debug($"Log file location = {Directory.GetCurrentDirectory()}");
    NumberWorker worker = ActivatorUtilities.CreateInstance<NumberWorker>(host.Services);
    worker.PrintNumber();
    worker.PrintOtherSettings();
    Log.CloseAndFlush();
  }

  private static IHost CreateHost() {
    Log.Logger = new LoggerConfiguration().CreateLogger();
    return Host.CreateDefaultBuilder()
      .ConfigureServices((context, services) => {
        services.Configure<NumberConfig>(context.Configuration.GetSection("Number"));
        services.Configure<OtherSettingsConfig>(context.Configuration.GetSection("OtherSettings"));
        services.AddSingleton<INumberRepository, NumberRepository>();
        services.AddSingleton<INumberService, NumberService>();
      })
      .UseSerilog((hostBuilderContext, serviceProvider, loggerConfiguration) =>
          loggerConfiguration
            .ReadFrom.Configuration(hostBuilderContext.Configuration)
            .ReadFrom.Services(serviceProvider)
            // .Enrich.FromLogContext()
      )
      .Build();
  }
}