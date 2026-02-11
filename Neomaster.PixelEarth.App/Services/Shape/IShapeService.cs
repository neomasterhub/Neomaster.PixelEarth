using Neomaster.PixelEarth.Utils;

namespace Neomaster.PixelEarth.App;

public interface IShapeService
{
  void DrawTextureRectangle(Rectangle rectangle, TextureShapeOptions? shapeOptions = null);
  void DrawTextureTriangle(Triangle triangle, TextureShapeOptions? shapeOptions = null);
  void DrawColorRectangle(Rectangle rectangle, ColorShapeOptions? shapeOptions = null);
  void DrawColorTriangle(Triangle triangle, ColorShapeOptions? shapeOptions = null);
  void InitializeBuffers();
}
