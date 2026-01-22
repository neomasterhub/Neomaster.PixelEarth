using System.Numerics;

namespace Neomaster.PixelEarth.App;

public record WindowSettings
{
  public int Width { get; init; }
  public int Height { get; init; }
  public string Title { get; init; }
  public Vector4 BackgroundColor { get; init; }
}
