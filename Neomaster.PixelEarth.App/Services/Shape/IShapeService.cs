using Neomaster.PixelEarth.Utils;

namespace Neomaster.PixelEarth.App;

public interface IShapeService
{
  void DrawTextureRectangle(Rectangle rectangle, TextureShapeOptions shapeOptions);
  void DrawTextureTriangle(Triangle triangle, TextureShapeOptions shapeOptions);
  void DrawColorRectangle(Rectangle rectangle, ColorShapeOptions? shapeOptions = null);
  void DrawColorTriangle(Triangle triangle, ColorShapeOptions? shapeOptions = null);
  void InitializeBuffers();
}
