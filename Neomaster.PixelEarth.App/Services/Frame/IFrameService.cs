namespace Neomaster.PixelEarth.App;

public interface IFrameService
{
  FrameInfo FrameInfo { get; }
  void BeginFrame();
  void ResetFrame();
}
