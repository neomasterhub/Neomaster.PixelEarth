namespace Neomaster.PixelEarth.App;

public struct TextureShapeOptions
{
  public int TextureId;
  public int TextureIdHovered;
  public int TextureIdSelected;
  public int TextureIdSelectedHovered;
  public bool IsHovered;
  public bool IsSelected;

  public TextureShapeOptions(
    Texture normal,
    Texture hovered = null,
    Texture selected = null,
    Texture selectedHovered = null)
  {
    ArgumentNullException.ThrowIfNull(normal);

    if (!normal.IsLoaded || normal.LoadedId < 1)
    {
      throw new ArgumentException("The normal texture must be loaded and have a valid LoadedId.", nameof(normal));
    }

    TextureId = normal.LoadedId;
    TextureIdHovered = hovered?.LoadedId ?? TextureId;
    TextureIdSelected = selected?.LoadedId ?? TextureId;
    TextureIdSelectedHovered = selectedHovered?.LoadedId ?? TextureIdSelected;
  }

  public readonly int CurrentTextureId => IsHovered
    ? (IsSelected ? TextureIdSelectedHovered : TextureIdHovered)
    : (IsSelected ? TextureIdSelected : TextureId);

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
