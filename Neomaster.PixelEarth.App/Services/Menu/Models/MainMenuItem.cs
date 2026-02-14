namespace Neomaster.PixelEarth.App;

public record MainMenuItem
{
  public Button Button { get; init; }
  public Action DrawButton { get; init; }
}
