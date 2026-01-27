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
  public Vector4 TextureUvXYWidthHeightPrc;
  public Vector4 TextureHoveredUvXYWidthHeightPrc;
  public Vector4 TextureSelectedUvXYWidthHeightPrc;
  public Vector4 TextureSelectedHoveredUvXYWidthHeightPrc;
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

    hovered ??= normal;
    selected ??= normal;
    selectedHovered ??= normal;

    TextureId = normal.LoadedId;
    TextureHoveredId = hovered.LoadedId;
    TextureSelectedId = selected.LoadedId;
    TextureSelectedHoveredId = selectedHovered.LoadedId;

    TextureUvXYWidthHeight = textureUvXYWidthHeight ?? new Vector4(0, 0, normal.Width, normal.Height);
    TextureHoveredUvXYWidthHeight = textureHoveredUvXYWidthHeight ?? new Vector4(0, 0, hovered.Width, hovered.Height);
    TextureSelectedUvXYWidthHeight = textureSelectedUvXYWidthHeight ?? new Vector4(0, 0, selected.Width, selected.Height);
    TextureSelectedHoveredUvXYWidthHeight = textureSelectedHoveredUvXYWidthHeight ?? new Vector4(0, 0, selectedHovered.Width, selectedHovered.Height);

    TextureUvXYWidthHeightPrc = new Vector4(
      TextureUvXYWidthHeight.X / normal.Width,
      TextureUvXYWidthHeight.Y / normal.Height,
      TextureUvXYWidthHeight.Z / normal.Width,
      TextureUvXYWidthHeight.W / normal.Height);
    TextureHoveredUvXYWidthHeightPrc = new Vector4(
      TextureHoveredUvXYWidthHeight.X / hovered.Width,
      TextureHoveredUvXYWidthHeight.Y / hovered.Height,
      TextureHoveredUvXYWidthHeight.Z / hovered.Width,
      TextureHoveredUvXYWidthHeight.W / hovered.Height);
    TextureSelectedUvXYWidthHeightPrc = new Vector4(
      TextureSelectedUvXYWidthHeight.X / selected.Width,
      TextureSelectedUvXYWidthHeight.Y / selected.Height,
      TextureSelectedUvXYWidthHeight.Z / selected.Width,
      TextureSelectedUvXYWidthHeight.W / selected.Height);
    TextureSelectedHoveredUvXYWidthHeightPrc = new Vector4(
      TextureSelectedHoveredUvXYWidthHeight.X / selectedHovered.Width,
      TextureSelectedHoveredUvXYWidthHeight.Y / selectedHovered.Height,
      TextureSelectedHoveredUvXYWidthHeight.Z / selectedHovered.Width,
      TextureSelectedHoveredUvXYWidthHeight.W / selectedHovered.Height);
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
