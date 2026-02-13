using Neomaster.PixelEarth.App;
using Neomaster.PixelEarth.Utils;

namespace Neomaster.PixelEarth.Presentation;

public class RectangleTextureButtonBuilder
{
  private readonly Texture _textureMap;
  private float _x;
  private float _y;
  private float? _width;
  private float? _height;
  private Rectangle? _uvPx;
  private Rectangle? _uvHoveredPx;
  private Rectangle? _uvSelectedPx;
  private Rectangle? _uvSelectedHoveredPx;

  private RectangleTextureButtonBuilder(Texture map)
  {
    _textureMap = map;
  }

  public static RectangleTextureButtonBuilder Create(Texture map)
  {
    return new(map);
  }

  public RectangleTextureButtonBuilder Position(float x, float y)
  {
    _x = x;
    _y = y;

    return this;
  }

  public RectangleTextureButtonBuilder Size(float width, float height)
  {
    _width = width;
    _height = height;

    return this;
  }

  public RectangleTextureButtonBuilder UvPx(
    float x,
    float y,
    float? width = null,
    float? height = null)
  {
    _uvPx = new Rectangle(
      x,
      y,
      width ?? _width ?? _textureMap.Width,
      height ?? _height ?? _textureMap.Height);

    return this;
  }

  public RectangleTextureButtonBuilder UvHoveredPx(
    float x,
    float y,
    float? width = null,
    float? height = null)
  {
    _uvHoveredPx = new Rectangle(
      x,
      y,
      width ?? _width ?? _textureMap.Width,
      height ?? _height ?? _textureMap.Height);

    return this;
  }

  public RectangleTextureButtonBuilder UvSelectedPx(
    float x,
    float y,
    float? width = null,
    float? height = null)
  {
    _uvSelectedPx = new Rectangle(
      x,
      y,
      width ?? _width ?? _textureMap.Width,
      height ?? _height ?? _textureMap.Height);

    return this;
  }

  public RectangleTextureButtonBuilder UvSelectedHoveredPx(
    float x,
    float y,
    float? width = null,
    float? height = null)
  {
    _uvSelectedHoveredPx = new Rectangle(
      x,
      y,
      width ?? _width ?? _textureMap.Width,
      height ?? _height ?? _textureMap.Height);

    return this;
  }

  public RectangleTextureButton Build(int id)
  {
    return new RectangleTextureButton(id)
    {
      X = _x,
      Y = _y,
      Width = _width ?? _textureMap.Width,
      Height = _height ?? _textureMap.Height,
      TextureShapeOptions = new(
        _textureMap,
        _uvPx,
        _uvHoveredPx,
        _uvSelectedPx,
        _uvSelectedHoveredPx),
    };
  }
}
