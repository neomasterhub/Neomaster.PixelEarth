using System.Numerics;

namespace Neomaster.PixelEarth.App;

public interface IShapeService
{
  void DrawTextureRectangle(
    Vector4 xyWidthHeight,
    Vector4 uvXYWidthHeight,
    Vector4? uvHoveredXYWidthHeight = null,
    Vector4? uvSelectedXYWidthHeight = null,
    Vector4? uvSelectedHoveredXYWidthHeight = null,
    TextureShapeOptions? shapeOptions = null);

  void DrawTextureRectangle(
    float x,
    float y,
    float width,
    float height,
    float uvX,
    float uvY,
    float uvWidth,
    float uvHeight,
    TextureShapeOptions? shapeOptions = null);

  void DrawTextureRectangle(
    Vector2 topLeft,
    Vector2 bottomRight,
    Vector2 uvTopLeft,
    Vector2 uvBottomRight,
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

  void DrawColorTriangle(
    Vector2 a,
    Vector2 b,
    Vector2 c,
    ColorShapeOptions? shapeOptions = null);

  void InitializeBuffers();
}
