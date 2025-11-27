using System.Numerics;

namespace Neomaster.PixelEarth.Presentation;

public interface IShapeService
{
  void DrawTriangle(
    Vector2 a,
    Vector2 b,
    Vector2 c,
    ShapeOptions shapeOptions = null);

  void InitializeBuffers();
}
