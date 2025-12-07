namespace Neomaster.PixelEarth.Presentation;

public interface IFrameService
{
  FrameInfo FrameInfo { get; }
  void Reset();
  void Update(FrameInfo frameInfo);
}
