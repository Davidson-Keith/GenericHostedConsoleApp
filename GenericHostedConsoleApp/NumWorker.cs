using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Serilog;

namespace GenericHostedConsoleApp;

public class NumberWorker {
  private readonly ILogger<NumberWorker> logger;
  private readonly INumberService service;
  private readonly OtherSettingsConfig config;

  // public NumberRepository(IOptions<NumberConfig> options) => config = options.Value;

  public NumberWorker(ILogger<NumberWorker> logger, INumberService service, IOptions<OtherSettingsConfig> options) {
    this.logger = logger;
    this.service = service;
    config = options.Value;
  }

  public void PrintNumber() {
    logger.LogDebug("PrintNumber() called");
    Log.Debug("Global logger called by NumberWorker.PrintNumber()");
    logger.LogDebug("Local logger called by NumberWorker.PrintNumber()");
    var number = service.GetPositiveNumber();
    Console.WriteLine($"My wonderful number is {number}");
  }

  public void PrintOtherSettings() {
    logger.LogDebug("PrintOtherSettings() called");
    logger.LogDebug($"KeyOne = {config.KeyOne}");
    logger.LogDebug($"KeyTwo = {config.KeyTwo}");
    logger.LogDebug($"KeyThree.Message = {config.KeyThree.Message}");
    logger.LogDebug($"KeyThree.SupportedVersions.v1 = {config.KeyThree.SupportedVersions.v1}");
    logger.LogDebug($"KeyThree.SupportedVersions.v3 = {config.KeyThree.SupportedVersions.v3}");
  }
}
