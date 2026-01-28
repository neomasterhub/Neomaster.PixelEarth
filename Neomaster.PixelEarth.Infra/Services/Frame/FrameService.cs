using Neomaster.PixelEarth.App;

namespace Neomaster.PixelEarth.Infra;

public class FrameService : IFrameService
{
  public FrameInfo FrameInfo { get; private set; } = new();

  public void BeginFrame()
  {
    var currentHoveredId = FrameInfo.CurrentHoveredId;
    FrameInfo.CurrentHoveredId = FrameInfo.NextHoveredId;
    FrameInfo.NextHoveredId = currentHoveredId;
  }

  public void ResetFrame()
  {
    FrameInfo.CurrentHoveredId = -1;
    FrameInfo.NextHoveredId = -1;
  }
}
