using Microsoft.Extensions.Logging;
using Serilog;

namespace GenericHostedConsoleApp;

public class NumberWorker {
  private readonly ILogger<NumberWorker> logger;
  private readonly INumberService service;

  public NumberWorker(ILogger<NumberWorker> logger, INumberService service) {
    this.logger = logger;
    this.service = service;
  }

  public void PrintNumber() {
    logger.LogDebug("PrintNumber() called");
    Log.Debug("Global logger called by NumberWorker.PrintNumber()");
    logger.LogDebug("Local logger called by NumberWorker.PrintNumber()");
    var number = service.GetPositiveNumber();
    Console.WriteLine($"My wonderful number is {number}");
  }
}
