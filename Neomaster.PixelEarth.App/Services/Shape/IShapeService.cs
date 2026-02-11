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

  void DrawTextureTriangle(
    Vector2 a,
    Vector2 b,
    Vector2 c,
    TextureShapeOptions? shapeOptions = null);

  void DrawColorRectangle(
    float x,
    float y,
    float width,
    float height,
    ColorShapeOptions? shapeOptions = null);

  void DrawColorRectangle(
    Vector2 topLeft,
    Vector2 bottomRight,
    ColorShapeOptions? shapeOptions = null);

  void DrawColorTriangle(Triangle triangle, ColorShapeOptions? shapeOptions = null);

  void InitializeBuffers();
}
