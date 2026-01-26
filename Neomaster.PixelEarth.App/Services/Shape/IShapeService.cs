using System.Numerics;

namespace Neomaster.PixelEarth.App;

public interface IShapeService
{
  ShapeState DrawColorRectangle(
    float x,
    float y,
    float width,
    float height,
    ColorShapeOptions? shapeOptions = null);

  ShapeState DrawColorRectangle(
    Vector2 topLeft,
    Vector2 bottomRight,
    ColorShapeOptions? shapeOptions = null);

  ShapeState DrawTextureRectangle(
    float x,
    float y,
    float width,
    float height,
    float uvX,
    float uvY,
    float uvWidth,
    float uvHeight,
    TextureShapeOptions? shapeOptions = null);

  ShapeState DrawTextureRectangle(
    Vector2 topLeft,
    Vector2 bottomRight,
    Vector2 uvTopLeft,
    Vector2 uvBottomRight,
    TextureShapeOptions? shapeOptions = null);

  void DrawColorTriangle(
    Vector2 a,
    Vector2 b,
    Vector2 c,
    ColorShapeOptions? shapeOptions = null);

  void DrawTextureTriangle(
    Vector2 a,
    Vector2 b,
    Vector2 c,
    Vector2 uvA,
    Vector2 uvB,
    Vector2 uvC,
    TextureShapeOptions? shapeOptions = null);

  void InitializeBuffers();
}
