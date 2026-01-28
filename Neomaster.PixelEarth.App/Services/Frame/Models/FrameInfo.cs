namespace Neomaster.PixelEarth.App;

public record FrameInfo
{
  public int CurrentHoveredId { get; set; }
  public int NextHoveredId { get; set; }
  public int SelectedId { get; set; }
}
