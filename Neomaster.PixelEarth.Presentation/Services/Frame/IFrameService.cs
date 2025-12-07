namespace Neomaster.PixelEarth.Presentation;

public interface IFrameService
{
  FrameInfo FrameInfo { get; }
  void Update(FrameInfo frameInfo);
}
