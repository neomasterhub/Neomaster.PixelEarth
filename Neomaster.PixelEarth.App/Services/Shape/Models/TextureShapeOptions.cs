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
    Texture map,
    Vector2[] uvPx,
    Vector2[] uvHoveredPx = null,
    Vector2[] uvSelectedPx = null,
    Vector2[] uvSelectedHoveredPx = null)
    : this(map, null, null, null, null, null, null)
  {
    UV = GetUvFromPx(uvPx, map.Width, map.Height);
    UVHovered = GetUvFromPx(uvHoveredPx, map.Width, map.Height) ?? UV;
    UVSelected = GetUvFromPx(uvSelectedPx, map.Width, map.Height) ?? UV;
    UVSelectedHovered = GetUvFromPx(uvSelectedHoveredPx, map.Width, map.Height) ?? UVSelected;
  }

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

    UV = uv;
    UVHovered = uvHovered ?? UV;
    UVSelected = uvSelected ?? UV;
    UVSelectedHovered = uvSelectedHovered ?? UVSelected;
  }

  public static TextureShapeOptions CreateForRectangle(
    Texture map,
    Rectangle uvPx,
    Rectangle? uvHoveredPx = null,
    Rectangle? uvSelectedPx = null,
    Rectangle? uvSelectedHoveredPx = null)
  {
    var vUvPx = uvPx.GetVerticies_TL_BR();
    var vUvHoveredPx = uvHoveredPx?.GetVerticies_TL_BR() ?? vUvPx;
    var vUvSelectedPx = uvSelectedPx?.GetVerticies_TL_BR() ?? vUvPx;
    var vUvSelectedHoveredPx = uvSelectedHoveredPx?.GetVerticies_TL_BR() ?? vUvSelectedPx;

    var opt = new TextureShapeOptions(map, vUvPx, vUvHoveredPx, vUvSelectedPx, vUvSelectedHoveredPx);

    return opt;
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
