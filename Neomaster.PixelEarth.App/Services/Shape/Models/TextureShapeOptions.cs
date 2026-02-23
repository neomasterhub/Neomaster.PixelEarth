using System.Numerics;
using Neomaster.PixelEarth.Utils;

namespace Neomaster.PixelEarth.App;

public struct TextureShapeOptions
{
  private static readonly Vector2[] _defaultUV = [new(0, 0), new(1, 1)];

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
  public Blending Blending;

  public TextureShapeOptions(
    Texture map,
    Rectangle? uvPx = null,
    Rectangle? uvHoveredPx = null,
    Rectangle? uvSelectedPx = null,
    Rectangle? uvSelectedHoveredPx = null,
    Blending blending = Blending.Alpha)
    : this(
        map,
        uvPx?.GetVerticies_TL_BR(),
        uvHoveredPx?.GetVerticies_TL_BR(),
        uvSelectedPx?.GetVerticies_TL_BR(),
        uvSelectedHoveredPx?.GetVerticies_TL_BR(),
        blending)
  {
  }

  public TextureShapeOptions(
    Texture map,
    Vector2[] uvPx = null,
    Vector2[] uvHoveredPx = null,
    Vector2[] uvSelectedPx = null,
    Vector2[] uvSelectedHoveredPx = null,
    Blending blending = Blending.Alpha)
    : this(
        map,
        null,
        null,
        null,
        GetUvFromPx(uvPx, map.Width, map.Height),
        GetUvFromPx(uvHoveredPx, map.Width, map.Height),
        GetUvFromPx(uvSelectedPx, map.Width, map.Height),
        GetUvFromPx(uvSelectedHoveredPx, map.Width, map.Height),
        blending)
  {
  }

  public TextureShapeOptions(
    Texture normal,
    Texture hovered = null,
    Texture selected = null,
    Texture selectedHovered = null,
    Vector2[] uv = null,
    Vector2[] uvHovered = null,
    Vector2[] uvSelected = null,
    Vector2[] uvSelectedHovered = null,
    Blending blending = Blending.Alpha)
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

    UV = uv ?? _defaultUV;
    UVHovered = uvHovered ?? UV;
    UVSelected = uvSelected ?? UV;
    UVSelectedHovered = uvSelectedHovered ?? UVSelected;

    Blending = blending;
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

  public readonly TextureShapeOptions FromRectangleForTriangle_TR()
  {
    var tr = this;

    tr.UV = new Rectangle(UV).GetTriangle_TR().GetVerticies();
    tr.UVHovered = new Rectangle(UVHovered).GetTriangle_TR().GetVerticies();
    tr.UVSelected = new Rectangle(UVSelected).GetTriangle_TR().GetVerticies();
    tr.UVSelectedHovered = new Rectangle(UVSelectedHovered).GetTriangle_TR().GetVerticies();

    return tr;
  }

  public readonly TextureShapeOptions FromRectangleForTriangle_BL()
  {
    var tr = this;

    tr.UV = new Rectangle(UV).GetTriangle_BL().GetVerticies();
    tr.UVHovered = new Rectangle(UVHovered).GetTriangle_BL().GetVerticies();
    tr.UVSelected = new Rectangle(UVSelected).GetTriangle_BL().GetVerticies();
    tr.UVSelectedHovered = new Rectangle(UVSelectedHovered).GetTriangle_BL().GetVerticies();

    return tr;
  }

  private static Vector2[] GetUvFromPx(Vector2[] uvPx, float width, float height)
  {
    return uvPx?.Select(uv => new Vector2(uv.X / width, uv.Y / height)).ToArray();
  }
}
