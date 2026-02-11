using System.Numerics;

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
}
