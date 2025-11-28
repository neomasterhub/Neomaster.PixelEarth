using System.Numerics;

namespace Neomaster.PixelEarth.Presentation;

public interface IShapeService
{
  public void DrawQuad(
    float x,
    float y,
    float width,
    float height,
    ShapeOptions shapeOptions = null);

  public void DrawQuad(
    Vector2 topLeft,
    Vector2 bottomRight,
    ShapeOptions shapeOptions = null);

  void DrawTriangle(
    Vector2 a,
    Vector2 b,
    Vector2 c,
    ShapeOptions shapeOptions = null);

  void InitializeBuffers();
}
