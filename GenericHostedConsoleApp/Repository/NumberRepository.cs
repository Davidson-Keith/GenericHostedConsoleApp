using Microsoft.Extensions.Options;

namespace GenericHostedConsoleApp;

public class NumberRepository : INumberRepository {
  private readonly NumberConfig config;

  public NumberRepository(IOptions<NumberConfig> options) => config = options.Value;

  public int GetNumber() => config.DefaultNumber;
  // {
  //   return -42;
  //   return config.DefaultNumber;
  // }
}