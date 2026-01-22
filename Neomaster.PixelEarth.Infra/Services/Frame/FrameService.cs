using Neomaster.PixelEarth.App;

namespace Neomaster.PixelEarth.Infra;

public class FrameService : IFrameService
{
  public FrameInfo FrameInfo { get; private set; } = new();
}
