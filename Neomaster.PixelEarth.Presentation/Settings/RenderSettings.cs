namespace Neomaster.PixelEarth.Presentation;

public record RenderSettings
{
  public WindingOrder WindingOrder { get; init; }
  public bool AlphaBlendingEnabled { get; init; } = true;
}
