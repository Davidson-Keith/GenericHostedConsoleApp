using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace GenericHostedConsoleApp;

class Program {
  static void Main(string[] args) {
    IHost host = CreateHost();
    Log.Logger.Debug($"Log file location = {Directory.GetCurrentDirectory()}");
    NumberWorker worker = ActivatorUtilities.CreateInstance<NumberWorker>(host.Services);
    worker.PrintNumber();
    Log.CloseAndFlush();
  }

  private static IHost CreateHost() {
    Log.Logger = new LoggerConfiguration().CreateLogger();
    return Host.CreateDefaultBuilder()
      .ConfigureServices((context, services) => {
        services.Configure<NumberConfig>(context.Configuration.GetSection("Number"));
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