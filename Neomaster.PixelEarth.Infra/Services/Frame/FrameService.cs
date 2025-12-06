using Neomaster.PixelEarth.Presentation;

namespace Neomaster.PixelEarth.Infra;

public class FrameService : IFrameService
{
  public FrameInfo FrameInfo { get; private set; } = new();

  public void Reset()
  {
    FrameInfo.HoveredIds.Clear();
  }

  public void Update(FrameInfo frameInfo)
  {
    FrameInfo = frameInfo;
  }
}
