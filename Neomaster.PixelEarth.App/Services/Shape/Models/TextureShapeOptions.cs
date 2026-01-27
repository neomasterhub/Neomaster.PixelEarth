using System.Numerics;

namespace Neomaster.PixelEarth.App;

public struct TextureShapeOptions
{
  public int TextureId;
  public int TextureHoveredId;
  public int TextureSelectedId;
  public int TextureSelectedHoveredId;
  public Vector4 TextureUvXYWidthHeight;
  public Vector4 TextureHoveredUvXYWidthHeight;
  public Vector4 TextureSelectedUvXYWidthHeight;
  public Vector4 TextureSelectedHoveredUvXYWidthHeight;
  public bool IsHovered;
  public bool IsSelected;

  public TextureShapeOptions(
    Texture map,
    Vector4 textureUvXYWidthHeight,
    Vector4? textureHoveredUvXYWidthHeight = null,
    Vector4? textureSelectedUvXYWidthHeight = null,
    Vector4? textureSelectedHoveredUvXYWidthHeight = null)
    : this(
      map,
      null,
      null,
      null,
      textureUvXYWidthHeight,
      textureHoveredUvXYWidthHeight,
      textureSelectedUvXYWidthHeight,
      textureSelectedHoveredUvXYWidthHeight)
  {
  }

  public TextureShapeOptions(
    Texture normal,
    Texture hovered = null,
    Texture selected = null,
    Texture selectedHovered = null,
    Vector4? textureUvXYWidthHeight = null,
    Vector4? textureHoveredUvXYWidthHeight = null,
    Vector4? textureSelectedUvXYWidthHeight = null,
    Vector4? textureSelectedHoveredUvXYWidthHeight = null)
  {
    ArgumentNullException.ThrowIfNull(normal);

    if (!normal.IsLoaded || normal.LoadedId < 1)
    {
      throw new ArgumentException("The normal texture must be loaded and have a valid LoadedId.", nameof(normal));
    }

    TextureId = normal.LoadedId;
    TextureHoveredId = hovered?.LoadedId ?? TextureId;
    TextureSelectedId = selected?.LoadedId ?? TextureId;
    TextureSelectedHoveredId = selectedHovered?.LoadedId ?? TextureId;

    TextureUvXYWidthHeight = textureUvXYWidthHeight ?? new Vector4(0, 0, normal.Width, normal.Height);

    TextureHoveredUvXYWidthHeight = textureHoveredUvXYWidthHeight
      ?? (hovered == null ? TextureUvXYWidthHeight : new Vector4(0, 0, hovered.Width, hovered.Height));

    TextureSelectedUvXYWidthHeight = textureSelectedUvXYWidthHeight
      ?? (selected == null ? TextureUvXYWidthHeight : new Vector4(0, 0, selected.Width, selected.Height));

    TextureSelectedHoveredUvXYWidthHeight = textureSelectedHoveredUvXYWidthHeight
      ?? (selectedHovered == null ? TextureUvXYWidthHeight : new Vector4(0, 0, selectedHovered.Width, selectedHovered.Height));
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
