using System.Numerics;

namespace Neomaster.PixelEarth.Presentation;

public interface IShapeService
{
  public ShapeState DrawRectangle(
    float x,
    float y,
    float width,
    float height,
    ColorShapeOptions? shapeOptions = null);

  public ShapeState DrawRectangle(
    Vector2 topLeft,
    Vector2 bottomRight,
    ColorShapeOptions? shapeOptions = null);

  void DrawTriangle(
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
