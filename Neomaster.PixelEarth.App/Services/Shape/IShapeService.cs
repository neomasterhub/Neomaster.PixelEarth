using System.Numerics;
using Neomaster.PixelEarth.Utils;

namespace Neomaster.PixelEarth.App;

public interface IShapeService
{
  void DrawTextureRectangle(
    float x,
    float y,
    float width,
    float height,
    TextureShapeOptions? shapeOptions = null);

  void DrawTextureRectangle(
    Vector2 topLeft,
    Vector2 bottomRight,
    TextureShapeOptions? shapeOptions = null);

  void DrawTextureTriangle(Triangle triangle, TextureShapeOptions? shapeOptions = null);
  void DrawColorRectangle(Rectangle rectangle, ColorShapeOptions? shapeOptions = null);
  void DrawColorTriangle(Triangle triangle, ColorShapeOptions? shapeOptions = null);
  void InitializeBuffers();
}
