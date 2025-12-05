using Neomaster.PixelEarth.Presentation;

namespace Neomaster.PixelEarth.Infra;

public class UIService(IShapeService shapeService)
  : IUIService
{
  public void DrawButton(
    float x,
    float y,
    float width,
    float height)
  {
    var si = shapeService.DrawRectangle(x, y, width, height);
  }
}
