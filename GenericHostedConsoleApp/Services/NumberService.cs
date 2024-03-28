using Microsoft.Extensions.Logging;
using Serilog;

namespace GenericHostedConsoleApp;

public class NumberService : INumberService {
  private readonly INumberRepository repo;
  private readonly ILogger<NumberService> logger;

  public NumberService(INumberRepository repo, ILogger<NumberService> logger) {
    this.repo = repo;
    this.logger = logger;
  }

  public int GetPositiveNumber() {
    Log.Logger.Warning("Global logger called by NumberService.GetPositiveNumber()");
    logger.LogWarning("Local logger called by NumberService.GetPositiveNumber()");
    int number = repo.GetNumber();
    return Math.Abs(number);
  }
}
