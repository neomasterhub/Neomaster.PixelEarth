using System.Numerics;
using Neomaster.PixelEarth.Utils;

namespace Neomaster.PixelEarth.App;

public struct TextureShapeOptions
{
  public int TextureId;
  public int TextureHoveredId;
  public int TextureSelectedId;
  public int TextureSelectedHoveredId;
  public Vector2[] UV;
  public Vector2[] UVHovered;
  public Vector2[] UVSelected;
  public Vector2[] UVSelectedHovered;
  public bool IsHovered;
  public bool IsSelected;

  public TextureShapeOptions(
    Texture normal,
    Texture hovered = null,
    Texture selected = null,
    Texture selectedHovered = null,
    Vector2[] uv = null,
    Vector2[] uvHovered = null,
    Vector2[] uvSelected = null,
    Vector2[] uvSelectedHovered = null)
  {
    ArgumentNullException.ThrowIfNull(normal);

    if (!normal.IsLoaded || normal.LoadedId < 1)
    {
      throw new ArgumentException("The normal texture must be loaded and have a valid LoadedId.", nameof(normal));
    }

    hovered ??= normal;
    selected ??= normal;
    selectedHovered ??= selected;

    TextureId = normal.LoadedId;
    TextureHoveredId = hovered.LoadedId;
    TextureSelectedId = selected.LoadedId;
    TextureSelectedHoveredId = selectedHovered.LoadedId;

    UV = uv ?? [];
    UVHovered = uvHovered ?? UV;
    UVSelected = uvSelected ?? UV;
    UVSelectedHovered = uvSelectedHovered ?? uvSelected;
  }

  public readonly TextureShapeState GetCurrentState()
  {
    TextureShapeState state;

    if (IsHovered)
    {
      if (IsSelected)
      {
        state = new TextureShapeState
        {
          TextureId = TextureSelectedHoveredId,
          UV = UVSelectedHovered,
        };
      }
      else
      {
        state = new TextureShapeState
        {
          TextureId = TextureHoveredId,
          UV = UVHovered,
        };
      }
    }
    else
    {
      if (IsSelected)
      {
        state = new TextureShapeState
        {
          TextureId = TextureSelectedId,
          UV = UVSelected,
        };
      }
      else
      {
        state = new TextureShapeState
        {
          TextureId = TextureId,
          UV = UV,
        };
      }
    }

    return state;
  }

  public TextureShapeOptions SetHovered(bool isHovered)
  {
    IsHovered = isHovered;
    return this;
  }

  public TextureShapeOptions SetSelected(bool isSelected)
  {
    IsSelected = isSelected;
    return this;
  }

  public readonly (TextureShapeOptions bottomLeft, TextureShapeOptions topRight) AsRectangleToTriangles()
  {
    var bottomLeft = this;
    bottomLeft.UV = MathX.Rectangle.GetTrianglePoints_BottomLeft(UV);
    bottomLeft.UVHovered = MathX.Rectangle.GetTrianglePoints_BottomLeft(UVHovered);
    bottomLeft.UVSelected = MathX.Rectangle.GetTrianglePoints_BottomLeft(UVSelected);
    bottomLeft.UVSelectedHovered = MathX.Rectangle.GetTrianglePoints_BottomLeft(UVSelectedHovered);

    var topRight = this;
    topRight.UV = MathX.Rectangle.GetTrianglePoints_TopRight(UV);
    topRight.UVHovered = MathX.Rectangle.GetTrianglePoints_TopRight(UVHovered);
    topRight.UVSelected = MathX.Rectangle.GetTrianglePoints_TopRight(UVSelected);
    topRight.UVSelectedHovered = MathX.Rectangle.GetTrianglePoints_TopRight(UVSelectedHovered);

    return (bottomLeft, topRight);
  }
}
