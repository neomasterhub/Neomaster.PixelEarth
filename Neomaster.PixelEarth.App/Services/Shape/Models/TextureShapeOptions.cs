using System.Numerics;

namespace Neomaster.PixelEarth.App;

public struct TextureShapeOptions
{
  public int TextureId;
  public int TextureHoveredId;
  public int TextureSelectedId;
  public int TextureSelectedHoveredId;
  public Vector4 UV;
  public Vector4 UVHovered;
  public Vector4 UVSelected;
  public Vector4 UVSelectedHovered;
  public bool IsHovered;
  public bool IsSelected;

  public TextureShapeOptions(
    Texture normal,
    Texture hovered = null,
    Texture selected = null,
    Texture selectedHovered = null,
    Vector4? uv = null,
    Vector4? uvHovered = null,
    Vector4? uvSelected = null,
    Vector4? uvSelectedHovered = null)
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

    uv ??= new Vector4(0, 0, 1, 1);
    uvHovered ??= uv;
    uvSelected ??= uv;
    uvSelectedHovered ??= uvSelected;

    UV = uv.Value;
    UVHovered = uvHovered.Value;
    UVSelected = uvSelected.Value;
    UVSelectedHovered = uvSelectedHovered.Value;
  }

  public readonly int CurrentTextureId => IsHovered
    ? (IsSelected ? TextureSelectedHoveredId : TextureHoveredId)
    : (IsSelected ? TextureSelectedId : TextureId);

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
