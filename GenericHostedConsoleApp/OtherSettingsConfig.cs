namespace GenericHostedConsoleApp;

public class OtherSettingsConfig {
  public required int KeyOne { get; set; }
  public required bool KeyTwo { get; set; }
  public required NestedSettings KeyThree { get; set; } = null!;
  public required string[] IPAddressRange { get; set; } = null!;
}

public sealed class NestedSettings {
  public required string Message { get; set; } = null!;
  public required DoubleNested SupportedVersions { get; set; } = null!;
}

public sealed class DoubleNested {
  public required string v1 { get; set; } = null!;
  public required string v3 { get; set; } = null!;
}

