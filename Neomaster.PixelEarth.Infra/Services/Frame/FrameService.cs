using Neomaster.PixelEarth.App;

namespace Neomaster.PixelEarth.Infra;

public class FrameService : IFrameService
{
  public FrameInfo FrameInfo { get; private set; } = new();

  public void BeginFrame()
  {
    FrameInfo.CurrentHoveredId = FrameInfo.NextHoveredId;
    FrameInfo.NextHoveredId = -1;
  }

  public void ResetFrame()
  {
    FrameInfo.CurrentHoveredId = -1;
    FrameInfo.NextHoveredId = -1;
  }
}
