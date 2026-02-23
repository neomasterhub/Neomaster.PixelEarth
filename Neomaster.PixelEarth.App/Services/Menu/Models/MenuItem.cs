namespace Neomaster.PixelEarth.App;

public record MenuItem
{
  public Button Button { get; init; }
  public Action DrawButton { get; init; }
}
