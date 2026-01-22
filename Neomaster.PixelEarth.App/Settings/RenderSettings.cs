namespace Neomaster.PixelEarth.App;

public record RenderSettings
{
  public WindingOrder WindingOrder { get; init; }
  public bool AlphaBlendingEnabled { get; init; }
}
